using FMDCore.Interfaces;
using FMDInfra.Data;
using FMDInfra.Models;
using FMDInfra.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDIntegrationTests
{

    public class ParticipantTestIntegration : IDisposable
    {
        private IParticipantRepository _repository = default!;
        private AppDbContext _appDbContext = default!;
        private ILectureRepository _lectureRepository = default!;

        public ParticipantTestIntegration()
        {
            _appDbContext = new SqliteDbContextFactory().Create();
            _repository = new ParticipantRepository(_appDbContext);
            _lectureRepository = new LectureRepository(_appDbContext);
        }

        [Fact(DisplayName = "Should create a participant in database with success")]
        public async Task ShouldCreateWithSuccess()
        {
            var lectureId = Guid.NewGuid();
            var lecture = new Lecture
            {
                Id = lectureId,
                DateTime = DateTime.Now,
                Description = "desc",
                Title = "title"
            };

            var participantId = Guid.NewGuid();
            var participant = new Participant
            {
                Id = participantId,
                Name = "name",
                Email = "x@c.com",
                Phone = "1234567890",
                LectureId = lectureId
            };

            await _lectureRepository.AddAsync(lecture);
            var result = await _repository.AddAsync(participant);

            Assert.NotNull(result);
            Assert.Equal(participantId, result.Id);
            Assert.Equal(participant.Name, result.Name);
            Assert.Equal(participant.Email, result.Email);
            Assert.Equal(participant.Phone, result.Phone);
            Assert.Equal(lectureId, result.LectureId);
        }

        [Fact(DisplayName = "Should get all participants from database with success")]
        public async Task ShouldGetAllWithSuccess()
        {
            var lectureId = Guid.NewGuid();
            var lecture = new Lecture
            {
                Id = lectureId,
                DateTime = DateTime.Now,
                Description = "desc",
                Title = "title"
            };
            await _lectureRepository.AddAsync(lecture);

            var participant1 = new Participant
            {
                Id = Guid.NewGuid(),
                Name = "name1",
                Email = "a@c.com",
                Phone = "1111111111",
                LectureId = lectureId
            };

            var participant2 = new Participant
            {
                Id = Guid.NewGuid(),
                Name = "name2",
                Email = "b@c.com",
                Phone = "2222222222",
                LectureId = lectureId
            };

            await _repository.AddAsync(participant1);
            await _repository.AddAsync(participant2);

            var result = await _repository.GetAllAsync();

            var participants = result.ToList();

            var foundParticipant1 = participants.FirstOrDefault(p => p.Id == participant1.Id);
            var foundParticipant2 = participants.FirstOrDefault(p => p.Id == participant2.Id);

            Assert.NotNull(result);
            Assert.True(participants.Count >= 2);

            Assert.NotNull(foundParticipant1);
            Assert.Equal(participant1.Name, foundParticipant1.Name);
            Assert.Equal(participant1.Email, foundParticipant1.Email);
            Assert.Equal(participant1.Phone, foundParticipant1.Phone);
            Assert.Equal(lectureId, foundParticipant1.LectureId);

            Assert.NotNull(foundParticipant2);
            Assert.Equal(participant2.Name, foundParticipant2.Name);
            Assert.Equal(participant2.Email, foundParticipant2.Email);
            Assert.Equal(participant2.Phone, foundParticipant2.Phone);
            Assert.Equal(lectureId, foundParticipant2.LectureId);
        }

        [Fact(DisplayName = "Should update a participant in database with success")]
        public async Task ShouldUpdateWithSuccess()
        {
            var lectureId = Guid.NewGuid();
            var lecture = new Lecture
            {
                Id = lectureId,
                DateTime = DateTime.Now,
                Description = "desc",
                Title = "title"
            };
            await _lectureRepository.AddAsync(lecture);

            var participantId = Guid.NewGuid();
            var participant = new Participant
            {
                Id = participantId,
                Name = "name",
                Email = "x@c.com",
                Phone = "1234567890",
                LectureId = lectureId
            };
            await _repository.AddAsync(participant);

            participant.Name = "updated name";
            participant.Email = "updated@c.com";
            participant.Phone = "0987654321";

            var updatedParticipant = await _repository.UpdateAsync(participant);

            Assert.NotNull(updatedParticipant);
            Assert.Equal(participantId, updatedParticipant.Id);
            Assert.Equal(participant.Name, updatedParticipant.Name);
            Assert.Equal(participant.Email, updatedParticipant.Email);
            Assert.Equal(participant.Phone, updatedParticipant.Phone);
            Assert.Equal(lectureId, updatedParticipant.LectureId);
        }

        [Fact(DisplayName = "Should delete a participant from database with success")]
        public async Task ShouldDeleteWithSuccess()
        {
            var lectureId = Guid.NewGuid();
            var lecture = new Lecture
            {
                Id = lectureId,
                DateTime = DateTime.Now,
                Description = "desc",
                Title = "title"
            };
            await _lectureRepository.AddAsync(lecture);

            var participantId = Guid.NewGuid();
            var participant = new Participant
            {
                Id = participantId,
                Name = "name",
                Email = "x@c.com",
                Phone = "1234567890",
                LectureId = lectureId
            };
            await _repository.AddAsync(participant);

            var deleteResult = await _repository.DeleteAsync(participantId);

            var allParticipants = await _repository.GetAllAsync();
            var foundParticipant = allParticipants.FirstOrDefault(p => p.Id == participantId);

            Assert.True(deleteResult);
            Assert.Null(foundParticipant);
        }

        [Fact(DisplayName = "Should not create two repeated participants")]
        public async Task ShouldNotCreateRepeatedPartcipants()
        {
            var lectureId = Guid.NewGuid();
            var lecture = new Lecture
            {
                Id = lectureId,
                DateTime = DateTime.Now,
                Description = "desc",
                Title = "title"
            };
            await _lectureRepository.AddAsync(lecture);

            var participantId = Guid.NewGuid();
            var participant = new Participant
            {
                Id = participantId,
                Name = "name",
                Email = "x@c.com",
                Phone = "1234567890",
                LectureId = lectureId
            };

            var participant2 = new Participant
            {
                Id = participantId,
                Name = "name",
                Email = "x@c.com",
                Phone = "1234567890",
                LectureId = lectureId
            };

            await _repository.AddAsync(participant);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.AddAsync(participant2));
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
