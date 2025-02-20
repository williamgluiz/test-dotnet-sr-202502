using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApplicantTracking.Api.Controllers
{
    [ApiController]
    [Route("candidates")]
    public sealed class CandidateController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> List()
        {
            // TODO: Implement this method
            return Ok();
        }

        [HttpGet("{idCandidate:int}")]
        public async Task<IActionResult> Get([FromRoute] int idCandidate)
        {
            // TODO: Implement this method
            return Ok();
        }

        // TODO: Change 'object candidate' to your viewmodel
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] object candidate)
        {
            // TODO: Implement this method
            return Ok();
        }

        // TODO: Change 'object candidate' to your viewmodel
        [HttpPut("{idCandidate:int}")]
        public async Task<IActionResult> Edit([FromRoute] int idCandidate, [FromBody] object candidate)
        {
            // TODO: Implement this method
            return NoContent();
        }

        [HttpDelete("{idCandidate:int}")]
        public async Task<IActionResult> Delete([FromRoute] int idCandidate)
        {
            // TODO: Implement this method
            return NoContent();
        }
    }
}
