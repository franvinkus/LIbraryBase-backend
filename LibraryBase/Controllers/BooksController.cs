using System.Security.Claims;
using FluentValidation;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryBase.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IValidator<PostBooksQuery> _postBooksValidator;
        public readonly IValidator<PutBooksQuery> _putBooksValidator;
        public readonly IValidator<DeleteBooksQuery> _deleteBooksValidator;
        public BooksController(IMediator mediator, IValidator<PostBooksQuery> postBooksValidator, IValidator<PutBooksQuery> putBooksValidator, IValidator<DeleteBooksQuery> deleteBooksValidator)
        {
            _mediator = mediator;
            _postBooksValidator = postBooksValidator;
            _putBooksValidator = putBooksValidator;
            _deleteBooksValidator = deleteBooksValidator;
            
        }
        // GET: api/<BooksController>
        [HttpGet("Get-Books")]
        public async Task<IActionResult> Get()
        {
            var request = new GetBooksQuery();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // POST api/<BooksController>
        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Book")]
        public async Task<IActionResult> Post([FromForm] PostBooksQuery dto)
        {
            var validation = _postBooksValidator.Validate(dto);
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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Unauthorized: UserId not found" });
            }

            var adminId = int.Parse(userId);
            var result = await _mediator.Send(dto);
            return Ok(result);
        }

        // PUT api/<BooksController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("Edit-Book/{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] PutBooksQuery dto)
        {
            var validation = _putBooksValidator.Validate(dto);
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

            var result = await _mediator.Send(new PutBooksQueryWithId
            {
                bookId = id,
                categoryIds = dto.categoryIds,
                title = dto.title,
                author = dto.author,
                description = dto.description
            });
            return Ok(result);
        }

        // DELETE api/<BooksController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Book/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = new DeleteBooksQuery(id);
            var validation = _deleteBooksValidator.Validate(delete);
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

            var result = await _mediator.Send(delete);
            return Ok(result);
        }
    }
}
