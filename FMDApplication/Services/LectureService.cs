using FMDApplication.Dtos;
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

        public async Task<ApiResponse<LectureDto>> AddAsync(LectureDto lecture)
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

            if (created == null)
                return new ApiResponse<LectureDto>(null, false, "Failed to create lecture");

            return new ApiResponse<LectureDto>(lecture, true, "Lecture created successfully");
        }

        public async Task<ApiResponse<IEnumerable<LectureDto>>> GetAllAsync()
        {
            var lectures = await _repository.GetAllAsync();
            return new ApiResponse<IEnumerable<LectureDto>>(lectures.Select(s => new LectureDto
            {
                Title = s.Title,
                Description = s.Description,
                DateTime = s.DateTime,
                Participants = s.Participants.Select(p => new ParticipantDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    Phone = p.Phone
                })
            }), true, "Lectures retrieved successfully");
        }
    }
}
