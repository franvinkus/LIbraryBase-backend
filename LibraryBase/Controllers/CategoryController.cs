using Azure.Core;
using FluentValidation;
using Library.Entities;
using LibraryBase.Model;
using LibraryBase.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryBase.Controllers
{
    [Route("api/LibraryBase/CRUD")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IValidator<PostCategoryQuery> _postCateValidator;
        public readonly IValidator<PutCategoryQuery> _putCateValidator;
        public readonly IValidator<DeleteCategoryQuery> _deleteCateValidator;

        public CategoryController(IMediator mediator, IValidator<PostCategoryQuery> postCateValidator, IValidator<PutCategoryQuery> putCateValidator, IValidator<DeleteCategoryQuery> deleteCateValidator)
        {
            _mediator = mediator;
            _postCateValidator = postCateValidator;
            _putCateValidator = putCateValidator;
            _deleteCateValidator = deleteCateValidator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Category")]
        public async Task<IActionResult> Get()
        {
            var query = new GetCategoryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST api/<AdminCRUDController>
        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Category")]
        public async Task<IActionResult> Post([FromBody] PostCategoryQuery dto)
        {
            var validation = _postCateValidator.Validate(dto);
            var adminId = HttpContext.Session.GetInt32("AdminID");
            if (adminId == null)
            {
                return Unauthorized(new { message = "Admin belum login" });
            }

            var cmd = new PostCategoryCommand
            {
                categoryName = dto.categoryName,
                createdBy = adminId.Value

            };
            var result = await _mediator.Send(dto);
            return Ok(result);
        }

        // PUT api/<AdminCRUDController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("Edit-Category/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PutCategoryQuery dto)
        {
            var validation = _putCateValidator.Validate(dto);
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
            var result = await _mediator.Send(dto);
            return Ok(result);
        }

        // DELETE api/<AdminCRUDController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = new DeleteCategoryQuery(id);
            var validation = _deleteCateValidator.Validate(query);
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

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
