using FMDApplication.Dtos.Lecture;
using FMDApplication.Response;

namespace FMDApplication.Services.Interfaces
{
    public interface ILectureService
    {
        Task<PagedResponse<IEnumerable<GetAllLectureDto>>> GetAllAsync(int pageNumber, int pageSize);
        Task<ApiResponse<CreateLectureOutuputDto>> AddAsync(CreateLectureInputDto lecture);
    }
}
