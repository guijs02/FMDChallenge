using FMDApplication.Dtos.Lecture;
using FMDApplication.Response;

namespace FMDApplication.Services
{
    public interface ILectureService
    {
        Task<ApiResponse<IEnumerable<GetAllLectureDto>>> GetAllAsync();
        Task<ApiResponse<CreateLectureOutuputDto>> AddAsync(CreateLectureInputDto lecture);
    }
}
