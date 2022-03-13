using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.Interfaces;
using SmartManager.Services.Services;

namespace SmartManager.API.Controllers
{
    [ApiController]

    public class UserController : ControllerBase {
        
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> getAllUsers() {
            var users = await _service.SearchByEmail("email@gmail.com");

            return Ok(users);
        }
    }
}