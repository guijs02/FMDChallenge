using FMDApplication.Dtos;
using FMDApplication.Dtos.Participant;
using FMDApplication.Services;
using FMDCore.Interfaces;
using FMDInfra.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDUnitTests
{
    public class ParticipantTests
    {
        private readonly Mock<IParticipantRepository> participantRepo;
        private readonly ParticipantService participantService;

        public ParticipantTests()
        {
            participantRepo = new Mock<IParticipantRepository>();
            participantService = new ParticipantService(participantRepo.Object);
        }

        [Fact(DisplayName = "Should create a Participant with success")]
        public async Task ShouldCreateParticipant()
        {
            var dto = new CreateParticipantInputDto
            {
                Name = "Jane Doe",
                Email = "jane@x.com",
                Phone = "9876543210"
            };

            participantRepo.Setup(x => x.AddAsync(It.IsAny<Participant>()))
                .ReturnsAsync(new Participant
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone
                });

            var response = await participantService.AddAsync(dto);

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(dto.Name, response.Data?.Name);
            Assert.Equal(dto.Email, response.Data?.Email);
            Assert.Equal(dto.Phone, response.Data?.Phone);

            participantRepo.Verify(x => x.AddAsync(It.IsAny<Participant>()), Times.Once);
        }

        [Fact(DisplayName = "Should get all Participants with success")]
        public async Task ShouldGetAllParticipants()
        {
            var participants = new List<Participant>
                {
                    new Participant
                    {
                        Id = Guid.NewGuid(),
                        Name = "Participant 1",
                        Email = "p1@x.com",
                        Phone = "1234567890"
                    }
                };

            participantRepo.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(participants);

            var response = await participantService.GetAllAsync(1, 10);

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.NotEmpty(response.Data);
            Assert.Equal(participants.Count, response.Data?.Count());
            Assert.Equal(participants.First().Name, response.Data?.First().Name);
            Assert.Equal(participants.First().Email, response.Data?.First().Email);
            Assert.Equal(participants.First().Phone, response.Data?.First().Phone);

            participantRepo.Verify(x => x.GetAllAsync(1, 10), Times.Once);
        }

        [Fact(DisplayName = "Should update a Participant with success")]
        public async Task ShouldUpdateParticipant()
        {
            var id = Guid.NewGuid();
            var dto = new UpdateParticipantInputDto
            {
                Name = "Updated Name",
                Email = "updated@x.com",
                Phone = "1112223333"
            };

            participantRepo.Setup(x => x.UpdateAsync(It.IsAny<Participant>()))
                .ReturnsAsync(new Participant
                {
                    Id = id,
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone
                });

            var response = await participantService.UpdateAsync(id,dto);

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(dto.Name, response.Data?.Name);
            Assert.Equal(dto.Email, response.Data?.Email);
            Assert.Equal(dto.Phone, response.Data?.Phone);

            participantRepo.Verify(x => x.UpdateAsync(It.IsAny<Participant>()), Times.Once);
        }

        [Fact(DisplayName = "Should delete a Participant with success")]
        public async Task ShouldDeleteParticipant()
        {
            var id = Guid.NewGuid();

            participantRepo.Setup(x => x.DeleteAsync(id)).ReturnsAsync(true);

            var response = await participantService.DeleteAsync(id);

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.True(response.Data);

            participantRepo.Verify(x => x.DeleteAsync(id), Times.Once);
        }
    }
}
