using System.Threading;
using FluentValidation;
using LibraryBase.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryBase.Controllers
{
    [Route("api/Borrow")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public readonly IValidator<DeleteBookingQuery> _validator;

        public BookingController(IMediator mediator, IValidator<DeleteBookingQuery> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        // GET: api/<BookingController>
        [Authorize(Roles = "Customer")]
        [HttpGet("See-All-Borrow")]
        public async Task<IActionResult> Get()
        {
            var request = new GetBookingQuery();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // POST api/<BookingController>
        [Authorize(Roles = "Customer")]
        [HttpPost("{bookingId}/Request-Borrow")]
        public async Task<IActionResult> Post(int bookingId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new PostBookingQuery(bookingId), cancellationToken);
            return Ok(result);
        }

        // PUT api/<BookingController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}/{bookingId}/Borrow")]
        public async Task<IActionResult> Borrow(int userId,int bookingId, CancellationToken cancellationToken)
        {
            if (bookingId <= 0)
            {
                return BadRequest("Invalid book ID.");
            }

            var result = await _mediator.Send(new PutBorrowBookingQuery(userId, bookingId), cancellationToken);
            return Ok(result);
        }

        // PUT api/<BookingController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}/{bookingId}/Return")]
        public async Task<IActionResult> Return(int userId, int bookingId, CancellationToken cancellationToken)
        {
            if (bookingId <= 0)
            {
                return BadRequest("Invalid book ID.");
            }

            var result = await _mediator.Send(new PutReturnBookingQuery(userId, bookingId), cancellationToken);
            return Ok(result);
        }

        // DELETE api/<BookingController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}/{bookingId}/Cancel")]
        public async Task<IActionResult> Delete(int userId, int bookingId)
        {
            var validation = _validator.Validate(new DeleteBookingQuery(userId,bookingId));

            if (!validation.IsValid)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = 404,
                    Type = "https://httpstatuses.com/404",
                    Title = "Data Tidak Sesuai",
                    Detail = $"Masukkan Data yang sesuai",
                    Instance = HttpContext.Request.Path,
                    Extensions = { ["errors"] = validation.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage) }
                });
            }

            var result = await _mediator.Send(new DeleteBookingQuery(userId, bookingId));
            return Ok(result);
        }
    }
}
