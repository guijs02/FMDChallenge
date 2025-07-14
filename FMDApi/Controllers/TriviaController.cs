using FMDApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace FMDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly ITriviaService _triviaService;
        public TriviaController(ITriviaService triviaService)
        {
            _triviaService = triviaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrivia() => Ok(await _triviaService.GetTriviaAsync()); 
            
    }
}
