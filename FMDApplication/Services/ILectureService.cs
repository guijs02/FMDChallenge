using FMDApplication.Dtos;
using FMDApplication.Response;

namespace FMDApplication.Services
{
    public interface ILectureService
    {
        Task<ApiResponse<IEnumerable<LectureDto>>> GetAllAsync();
        Task<ApiResponse<LectureDto>> AddAsync(LectureDto lecture);
    }
}
