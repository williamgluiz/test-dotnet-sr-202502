using ApplicantTracking.Application.Commands.Candidate.Create;
using ApplicantTracking.Application.Commands.Candidate.Delete;
using ApplicantTracking.Application.Commands.Candidate.Update;
using ApplicantTracking.Application.Queries.Candidate.GetAll;
using ApplicantTracking.Application.Queries.Candidate.GetById;
using ApplicantTracking.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicantTracking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of <see cref="CandidateController"/> with the injected mediator.
        /// </summary>
        /// <param name="mediator">Instance of <see cref="IMediator"/> to send commands and queries.</param>
        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns the full list of candidates.
        /// </summary>
        /// <returns>Returns 200 OK with the collection of candidates.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Candidate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Candidate>>> List()
        {
            return Ok(await _mediator.Send(new GetAllCandidatesQuery()));
        }

        /// <summary>
        /// Gets a candidate's data by their ID.
        /// </summary>
        /// <param name="id">ID of the candidate to be fetched.</param>
        /// <returns>
        /// Returns 200 OK with the candidate's data if found,
        /// or 404 Not Found if the candidate does not exist.
        /// </returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Candidate), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Candidate>> Get(int id)
        {
            if(id <= 0)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new GetCandidateByIdQuery(id));

            if (result is null)
                return NotFound();
                
            return Ok(result);
        }

        /// <summary>
        /// Creates a new candidate with the provided data.
        /// </summary>
        /// <param name="candidate">Command containing the data of the candidate to be created.</param>
        /// <returns>
        /// Returns 201 Created with the ID of the created candidate,
        /// or 400 Bad Request if the model is invalid.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateCandidateCommand candidate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _mediator.Send(candidate);

            return CreatedAtAction(nameof(Get), new {id}, id);
        }

        /// <summary>
        /// Updates the data of an existing candidate.
        /// </summary>
        /// <param name="id">ID of the candidate to be updated (must match the ID in the request body).</param>
        /// <param name="candidate">Command containing the updated candidate data.</param>
        /// <returns>
        /// Returns 204 No Content if the update is successful,
        /// 400 Bad Request if the route ID does not match the body ID,
        /// 404 Not Found if the candidate is not found.
        /// </returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateCandidateCommand candidate)
        {
            if (id != candidate.Id)
                return BadRequest("Id in the route and body do not match.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _mediator.Send(candidate);

            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a candidate by its ID.
        /// </summary>
        /// <param name="id">ID of the candidate to be deleted.</param>
        /// <returns>
        /// Returns 204 No Content if the deletion is successful,
        /// 404 Not Found if the candidate is not found,
        /// or 500 Internal Server Error in case of an unexpected error.
        /// </returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _mediator.Send(new DeleteCandidateCommand(id));

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
