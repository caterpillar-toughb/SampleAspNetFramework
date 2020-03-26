using MediatR;
using Microsoft.AspNetCore.Mvc;
using Outotec.Data.Business.Query;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Outotec.Data.Webservice.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/sample")]
    public class SampleController : Controller
    {
        private readonly IMediator _mediator;

        public SampleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Some sample documentation for the API.
        /// </summary>
        /// <param name="inputInt">Make sure it's greater than 0!</param>
        /// <response code="200">Returns the sample response</response>
        /// <response code="400">Poorly formatted input - input must be greater than 0.</response>
        [HttpGet("stuff")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetStuff([FromQuery] int inputInt)
        {
            if (inputInt < 0) return BadRequest();
            return Ok(await _mediator.Send(new GetSampleDataQuery()));
        }
    }
}