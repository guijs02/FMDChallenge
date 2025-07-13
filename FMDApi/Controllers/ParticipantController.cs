using FluentValidation;
using FMDApi.Extension;
using FMDApplication.Dtos;
using FMDApplication.Dtos.Participant;
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
        private readonly IValidator<CreateParticipantInputDto> _createValidator;
        private readonly IValidator<UpdateParticipantInputDto> _updateValidator;
        public ParticipantController(IParticipantService participantService,
                                    IValidator<CreateParticipantInputDto> createValidator,
                                    IValidator<UpdateParticipantInputDto> updateValidator)
        {
            _participantService = participantService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<GetAllParticipantDto>>>> GetAll()
        {
            var result = await _participantService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CreateParticipantOutputDto>>> Create([FromBody] CreateParticipantInputDto dto)
        {
            var validator = await _createValidator.ValidateAsync(dto);

            if (!validator.IsValid)
                return BadRequest(validator.FormatValidationErrors());

            var result = await _participantService.AddAsync(dto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UpdateParticipantOutputDto>>> Update(Guid id, [FromBody] UpdateParticipantInputDto dto)
        {
            var validator = await _updateValidator.ValidateAsync(dto);

            if (!validator.IsValid)
                return BadRequest(validator.FormatValidationErrors());

            var result = await _participantService.UpdateAsync(id, dto);
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
