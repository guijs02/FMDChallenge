using FMDApplication.Dtos;
using FMDApplication.Response;
using FMDCore.Interfaces;
using FMDInfra.Models;

namespace FMDApplication.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _repository;

        public ParticipantService(IParticipantRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<ParticipantDto?>> UpdateAsync(ParticipantDto dto)
        {
            var participant = new Participant
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                LectureId = dto.LectureId
            };

            var updated = await _repository.UpdateAsync(participant);
            if (updated == null)
                return new ApiResponse<ParticipantDto?>(null, false, "Participant not found");
            return new ApiResponse<ParticipantDto?>(dto, true, "Participant updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return new ApiResponse<bool>(false, false, "Participant not found");
            return new ApiResponse<bool>(true, true, "Participant deleted successfully");
        }


        public async Task<ApiResponse<ParticipantDto?>> AddAsync(ParticipantDto dto)
        {
            await _repository.AddAsync(new Participant
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                LectureId = dto.LectureId
            });

            return new ApiResponse<ParticipantDto?>(dto, true, "Participant created successfully");
        }

        public async Task<ApiResponse<IEnumerable<ParticipantDto>?>> GetAllAsync()
        {
            var particpants = await _repository.GetAllAsync();

            return new ApiResponse<IEnumerable<ParticipantDto>?>(
                particpants.Select(p => new ParticipantDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    Phone = p.Phone,
                    LectureId = p.LectureId
                }), true, "Participants retrieved successfully");
        }
    }
}
