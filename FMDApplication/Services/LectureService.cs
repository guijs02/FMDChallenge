using FMDApplication.Dtos;
using FMDApplication.Dtos.Lecture;
using FMDApplication.Dtos.Participant;
using FMDApplication.Response;
using FMDCore.Interfaces;

namespace FMDApplication.Services
{
    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _repository;

        public LectureService(ILectureRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<CreateLectureOutuputDto>> AddAsync(CreateLectureInputDto lecture)
        {

            var created = await _repository.AddAsync(new FMDInfra.Models.Lecture
            {
                Title = lecture.Title,
                Description = lecture.Description,
                DateTime = lecture.DateTime,
                Participants = lecture.Participants.Select(p => new FMDInfra.Models.Participant
                {
                    Name = p.Name,
                    Email = p.Email,
                    Phone = p.Phone
                }).ToList()
            });

            var output = new CreateLectureOutuputDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                DateTime = created.DateTime,
                Participants = created.Participants.Select(p => new CreateParticipantLectureOutputDto
                {
                    Id = p.Id,
                    LectureId = p.LectureId,
                    Name = p.Name,
                    Email = p.Email,
                    Phone = p.Phone
                })
            };

            return created == null ? new ApiResponse<CreateLectureOutuputDto>(null, false, "Failed to create lecture")
                           : new ApiResponse<CreateLectureOutuputDto>(output, true, "Lecture created successfully");
        }

        public async Task<ApiResponse<IEnumerable<GetAllLectureDto>>> GetAllAsync()
        {
            var lectures = await _repository.GetAllAsync();
            return new ApiResponse<IEnumerable<GetAllLectureDto>>(lectures.Select(s => new GetAllLectureDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                DateTime = s.DateTime,
                Participants = s.Participants.Select(p => new GetAllParticipantDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    Phone = p.Phone,
                    LectureId = p.LectureId
                })
            }), true, "Lectures retrieved successfully");
        }
    }
}
