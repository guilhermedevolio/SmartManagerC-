using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using SmartManager.Services.Services;

namespace SmartManager.API.Controllers
{
    [ApiController]

    public class UserController : ControllerBase {
        
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> getAllUsers() {
            var user = await _service.SearchByEmail("maria@gmail.com");

            return Ok(user);
        }
    }
}