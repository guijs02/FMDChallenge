using FMDApplication.Dtos;
using FMDApplication.Dtos.Lecture;
using FMDApplication.Dtos.Participant;
using FMDApplication.Services;
using FMDCore.Interfaces;
using FMDInfra.Persistence;
using Moq;

namespace FMDUnitTests
{
    public class LectureTests
    {
        private readonly Mock<ILectureRepository> lectureRepo;
        private readonly LectureService lectureService;
        public LectureTests()
        {
            lectureRepo = new Mock<ILectureRepository>();
            lectureService = new LectureService(lectureRepo.Object);
        }

        [Fact(DisplayName = "Should create a Lecture with success")]
        public async Task ShouldCreateLecture()
        {
            var dto = new CreateLectureInputDto
            {
                Title = "Test Lecture",
                Description = "This is a test lecture",
                DateTime = DateTime.Now,
                Participants = new List<CreateParticipantLectureInputDto>
                {
                    new() {
                        Name = "John Doe",
                        Email = "x@x.com",
                    }

                }
            };

            lectureRepo.Setup(x => x.AddAsync(It.IsAny<FMDInfra.Models.Lecture>()))
                .ReturnsAsync(new FMDInfra.Models.Lecture
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Description = dto.Description,
                    DateTime = dto.DateTime,
                    Participants = dto.Participants.Select(p => new FMDInfra.Models.Participant
                    {
                        Name = p.Name,
                        Email = p.Email,
                        Phone = p.Phone
                    }).ToList()
                });

            var response = await lectureService.AddAsync(dto);

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(dto.Title, response.Data?.Title);
            Assert.Equal(dto.Description, response.Data?.Description);
            Assert.Equal(dto.DateTime, response.Data?.DateTime);
            Assert.NotEmpty(response.Data?.Participants);
            Assert.Equal(dto.Participants.First().Name, response.Data?.Participants.First().Name);
            Assert.Equal(dto.Participants.First().Email, response.Data?.Participants.First().Email);
            Assert.Equal(dto.Participants.First().Phone, response.Data?.Participants.First().Phone);

            lectureRepo.Verify(x => x.AddAsync(It.IsAny<FMDInfra.Models.Lecture>()), Times.Once);
        }

        [Fact(DisplayName = "Should get all Lectures with success")]
        public async Task ShouldGetAllLectures()
        {
            var lectures = new List<FMDInfra.Models.Lecture>
            {
                new FMDInfra.Models.Lecture
                {
                    Id = Guid.NewGuid(),
                    Title = "Lecture 1",
                    Description = "Description 1",
                    DateTime = DateTime.Now,
                    Participants = new List<FMDInfra.Models.Participant>
                    {
                        new FMDInfra.Models.Participant
                        {
                            Name = "Participant 1",
                            Email = "xo@.com",
                            Phone = "1234567890"
                        }
                    }
                }
            };

            lectureRepo.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(lectures);

            var response = await lectureService.GetAllAsync(1, 10);

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.NotEmpty(response.Data);
            Assert.Equal(lectures.Count, response.Data?.Count());
            Assert.Equal(lectures.First().Title, response.Data?.First().Title);
            Assert.Equal(lectures.First().Description, response.Data?.First().Description);
            Assert.Equal(lectures.First().DateTime, response.Data?.First().DateTime);
            Assert.NotEmpty(response.Data?.First().Participants);
            Assert.Equal(lectures.First().Participants.First().Name, response.Data?.First().Participants.First().Name);
            Assert.Equal(lectures.First().Participants.First().Email, response.Data?.First().Participants.First().Email);
            Assert.Equal(lectures.First().Participants.First().Phone, response.Data?.First().Participants.First().Phone);
            lectureRepo.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}