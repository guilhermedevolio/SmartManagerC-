using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.Requests;
using SmartManager.Core.Exceptions;
using SmartManager.Services.Interfaces;

namespace SmartManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase {

        private IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> authenticate([FromBody] AuthenticateRequest request) {
            try {
                var auth = await _service.Authenticate(request);

                return Ok(auth);
            } catch (DomainException ex) {
                return Unauthorized(new {
                    Status = 1,
                    Message = ex.Message
                });
            }   
        }
    }
}