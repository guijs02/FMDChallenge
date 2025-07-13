using FMDApplication.Dtos;
using FMDApplication.Dtos.Participant;
using FMDApplication.Response;

namespace FMDApplication.Services
{
    public interface IParticipantService
    {

        Task<ApiResponse<UpdateParticipantOutputDto>> UpdateAsync(Guid id, UpdateParticipantInputDto dto);
        Task<ApiResponse<CreateParticipantOutputDto>> AddAsync(CreateParticipantInputDto dto);
        Task<ApiResponse<IEnumerable<GetAllParticipantDto>>> GetAllAsync();
        Task<ApiResponse<bool>> DeleteAsync(Guid id);

    }
}
