using FMDApplication.Dtos;
using FMDApplication.Dtos.Participant;
using FMDApplication.Response;

namespace FMDApplication.Services.Interfaces
{
    public interface IParticipantService
    {

        Task<ApiResponse<UpdateParticipantOutputDto>> UpdateAsync(Guid id, UpdateParticipantInputDto dto);
        Task<ApiResponse<CreateParticipantOutputDto>> AddAsync(CreateParticipantInputDto dto);
        Task<PagedResponse<IEnumerable<GetAllParticipantDto>>> GetAllAsync(int pageNumber, int pageSize);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);

    }
}
