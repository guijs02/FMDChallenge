using FMDApplication.Dtos;
using FMDApplication.Response;
using FMDApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace FMDApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ParticipantDto>>>> GetAll()
        {
            var result = await _participantService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ParticipantDto>>> Create([FromBody] ParticipantDto dto)
        {
            var result = await _participantService.AddAsync(dto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ParticipantDto>>> Update([FromBody] ParticipantDto dto)
        {
            var result = await _participantService.UpdateAsync(dto);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            var result = await _participantService.DeleteAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }
}
