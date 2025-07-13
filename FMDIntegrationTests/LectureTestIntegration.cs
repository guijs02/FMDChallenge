using FMDCore.Interfaces;
using FMDInfra.Data;
using FMDInfra.Models;
using FMDInfra.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FMDIntegrationTests
{
    public class LectureTestIntegration : IDisposable
    {
        private ILectureRepository _repository = default!;
        private AppDbContext _appDbContext = default!;
        public LectureTestIntegration()
        {
            _appDbContext = new SqliteDbContextFactory().Create();
            _repository = new LectureRepository(_appDbContext);
        }

        [Fact(DisplayName = "Should create a lecture in database with success")]
        public async Task ShouldCreateWithSuccess()
        {
            var lectureId = Guid.NewGuid();

            var lecture = new Lecture()
            {
                Id = lectureId,
                DateTime = DateTime.Now,
                Description = "desc",
                Title = "title",
                Participants = new List<Participant>()
                {
                    new Participant()
                    {
                        Email = "x@c.com",
                        Id = Guid.NewGuid(),
                        Name = "name",
                        Phone = "1234567890",
                        LectureId = lectureId

                    }
                }
            };


            var result = await _repository.AddAsync(lecture);

            Assert.NotNull(result);
            Assert.Equal(lectureId, result.Id);
            Assert.Equal(lecture.Title, result.Title);
            Assert.Equal(lecture.Description, result.Description);
            Assert.Equal(lecture.DateTime, result.DateTime);
            Assert.NotNull(result.Participants);
            Assert.Single(result.Participants);
            var participant = result.Participants.First();
            Assert.Equal(lecture.Participants.First().Email, participant.Email);
            Assert.Equal(lecture.Participants.First().Name, participant.Name);
            Assert.Equal(lecture.Participants.First().Phone, participant.Phone);
            Assert.Equal(lectureId, participant.LectureId);
            Assert.NotNull(result);
            Assert.Equal(lectureId, result.Id);
            Assert.Equal(lecture.Title, result.Title);
            
        }

        
        [Fact(DisplayName = "Should get all lectures from database with success")]
        public async Task ShouldGetAllWithSuccess()
        {
            var lectureId1 = Guid.NewGuid();
            var lectureId2 = Guid.NewGuid();

            var lecture1 = new Lecture()
            {
                Id = lectureId1,
                DateTime = DateTime.Now,
                Description = "desc1",
                Title = "title1",
                Participants = new List<Participant>()
                {
                    new Participant()
                    {
                        Email = "a@c.com",
                        Id = Guid.NewGuid(),
                        Name = "name1",
                        Phone = "1111111111",
                        LectureId = lectureId1
                    }
                }
            };

            var lecture2 = new Lecture()
            {
                Id = lectureId2,
                DateTime = DateTime.Now.AddDays(1),
                Description = "desc2",
                Title = "title2",
                Participants = new List<Participant>()
                {
                    new Participant()
                    {
                        Email = "b@c.com",
                        Id = Guid.NewGuid(),
                        Name = "name2",
                        Phone = "2222222222",
                        LectureId = lectureId2
                    }
                }
            };

            await _repository.AddAsync(lecture1);
            await _repository.AddAsync(lecture2);

            var result = await _repository.GetAllAsync();

            Assert.NotNull(result);
            var lectures = result.ToList();
            Assert.True(lectures.Count == 2);

            var foundLecture1 = lectures.FirstOrDefault(l => l.Id == lectureId1);
            var foundLecture2 = lectures.FirstOrDefault(l => l.Id == lectureId2);

            Assert.NotNull(foundLecture1);
            Assert.Equal(lecture1.Title, foundLecture1.Title);
            Assert.Equal(lecture1.Description, foundLecture1.Description);
            Assert.NotNull(foundLecture1.Participants);
            

            Assert.Single(foundLecture1.Participants);

            Assert.NotNull(foundLecture2);
            Assert.Equal(lecture2.Title, foundLecture2.Title);
            Assert.Equal(lecture2.Description, foundLecture2.Description);
            Assert.NotNull(foundLecture2.Participants);
            Assert.Single(foundLecture2.Participants);
        }
        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}