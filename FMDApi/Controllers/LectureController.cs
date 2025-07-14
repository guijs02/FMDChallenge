using FluentValidation;
using FMDApi.Extension;
using FMDApplication.Dtos.Lecture;
using FMDApplication.Response;
using FMDApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FMDApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lectureService;
        private readonly IValidator<CreateLectureInputDto> _validator;
        public LectureController(ILectureService lectureService, IValidator<CreateLectureInputDto> validator)
        {
            _lectureService = lectureService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CreateLectureInputDto>>>> GetAll(
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var result = await _lectureService.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateLectureInputDto>>> Create([FromBody] CreateLectureInputDto dto)
        {
            var validator = await _validator.ValidateAsync(dto);

            if (!validator.IsValid)
                return BadRequest(validator.FormatValidationErrors());

            var result = await _lectureService.AddAsync(dto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
