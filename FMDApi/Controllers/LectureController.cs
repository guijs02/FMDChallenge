using FMDApplication.Dtos;
using FMDApplication.Response;
using FMDApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace FMDApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LectureController : ControllerBase
    {
        private readonly LectureService _lectureService;

        public LectureController(LectureService lectureService)
        {
            _lectureService = lectureService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<LectureDto>>>> GetAll()
        {
            var result = await _lectureService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<LectureDto>>> Create([FromBody] LectureDto dto)
        {
            var result = await _lectureService.AddAsync(dto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
