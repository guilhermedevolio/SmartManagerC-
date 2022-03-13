using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartManager.Core.Exceptions;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using SmartManager.Services.Requests;
using SmartManager.Services.Services;

namespace SmartManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {
        
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-by-email")]
        public async Task<IActionResult> getByEmail([FromQuery] string email) {
            var user = await _service.SearchByEmail(email);

            if(user == null) {
                return Ok(new {
                    Status = 1,
                    Message = "Nenhum usuário encontrado com o email informado"
                });
            }

            return Ok(new {
                Status = 0,
                user
            });
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> create(createUserRequest request) {
            var userDTO = _mapper.Map<UserDTO>(request);

            try {
                var userCreated = await _service.CreateAsync(userDTO);

                return Ok(new {
                    Status = 0,
                    User = userCreated
                });

            } catch (DomainException ex) {
                return Ok(new {
                    Status = 1,
                    Message = ex.Message
                });
            }


        }
    }
}