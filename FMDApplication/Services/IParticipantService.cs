using FMDApplication.Dtos;
using FMDApplication.Response;

namespace FMDApplication.Services
{
    public interface IParticipantService
    {

        Task<ApiResponse<ParticipantDto?>> UpdateAsync(ParticipantDto dto);
        Task<ApiResponse<ParticipantDto?>> AddAsync(ParticipantDto dto);
        Task<ApiResponse<IEnumerable<ParticipantDto>?>> GetAllAsync();
        Task<ApiResponse<bool>> DeleteAsync(Guid id);

    }
}
