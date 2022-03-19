using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.Requests;
using SmartManager.Core.Exceptions;
using SmartManager.Services.Interfaces;
using SmartManager.API.Response;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using SmartManager.Infra.Interfaces;

namespace SmartManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase {

        private IAuthService _service;
        private IUserRepository _userRepository;

        public AuthController(IAuthService service, IUserRepository userRepository)
        {
            _service = service;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> authenticate([FromBody] AuthenticateRequest request) {

            try {
                var auth = await _service.Authenticate(request);

                return Ok(auth);
            } catch (DomainException ex) {
                 return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.GetErrors));
            }   
        }

        [HttpGet]
        [Authorize]
        [Route("validate-token")]
        public IActionResult validateToken() {
            String token = HttpContext.Request.Headers["Authorization"];
            String BearerToken = token.Split(' ')[1];

            try {
                var validateToken = _service.ValidateToken(BearerToken);

                return Ok(new {
                    Status = 0,
                    Valid = true
                });
            } catch (DomainException ex) {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.GetErrors));
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> refreshToken([FromBody] RefreshTokenRequest request) {
            try {
                var token = await _service.RefreshToken(request);
                return Ok(new {
                    Status = 0,
                    RefreshTokenUsed = token
                });
            } catch (DomainException ex) {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.GetErrors));
            }
        }

        
    }
}