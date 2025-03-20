using FluentValidation;
using Library.Entities;
using LibraryBase.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryBase.Controllers
{
    [Route("api/LibraryBase/Auth")]
    [ApiController]
    public class AuthContoller : ControllerBase
    {

        public readonly IMediator _mediator;
        public readonly IValidator<SignupCustomerQuery> _signupCustomerValidator;
        public readonly IValidator<LoginUserQuery> _loginValidator;
        public readonly IValidator<SignupAdminQuery> _signupAdminValidator;

        public AuthContoller(IMediator mediator, IValidator<SignupCustomerQuery> signupCustomerValidator, IValidator<LoginUserQuery> loginValidator, IValidator<SignupAdminQuery> signupAdminValidator)
        {
            _mediator = mediator;
            _signupCustomerValidator = signupCustomerValidator;
            _loginValidator = loginValidator;
            _signupAdminValidator = signupAdminValidator;
        }

        // POST api/<AuthContoller>
        [HttpPost("SignUp/Customer")]
        public async Task<IActionResult> Signup([FromBody] SignupCustomerQuery dto)
        {
            var validation = await _signupCustomerValidator.ValidateAsync(dto);
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
            var send = await _mediator.Send(dto);
            return Ok(send);
        }


        // POST api/<AuthContoller>
        [HttpPost("LogIn")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery dto)
        {
            var validation = await _loginValidator.ValidateAsync(dto);
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
            var send = await _mediator.Send(dto);
            return Ok(send);
        }

        [HttpPost("SignUp/Admin")]
        public async Task<IActionResult> SignupAdmin([FromBody] SignupAdminQuery dto)
        {
            var validation = await _signupAdminValidator.ValidateAsync(dto);
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
            var send = await _mediator.Send(dto);
            return Ok(send);
        }


    }
}
