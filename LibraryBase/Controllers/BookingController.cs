using System.Threading;
using LibraryBase.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryBase.Controllers
{
    [Route("api/Borrow")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BookingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        [HttpPost("{bookId}/Borrow")]
        public async Task<IActionResult> Post(int bookId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new PostBookingQuery(bookId), cancellationToken);
            return Ok(result);
        }

        // PUT api/<BookingController>/5
        [HttpPut("{bookId}/Return")]
        public async Task<IActionResult>Put(int bookId, CancellationToken cancellationToken)
        {
            if (bookId <= 0)
            {
                return BadRequest("Invalid book ID.");
            }

            var result = await _mediator.Send(new PutReturnBookingQuery(bookId), cancellationToken);
            return Ok(result);
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
