using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartManager.API.Response;
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
        [Authorize]
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

        [HttpGet]
        [Route("authenticated-user")]
        [Authorize]
        public  async Task<IActionResult> getAuthenticatedUser() {
             string email = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name).Value;

             var user = await _service.SearchByEmail(email);

             return Ok(new {
                 Status = 0,
                 user = user
             });
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
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

        [HttpGet]
        [Route("get-all")]
        [Authorize]
        public async Task<IActionResult> getAllUsers() {
            try {
                var users = await _service.GetAllAsync();

                return Ok(new {
                    Status = 0,
                    Users = users
                });

            } catch (DomainException ex) {
                return Ok(new {
                    Status = 1,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("advanced-search")]
        public async Task<IActionResult> advancedSearch(UserSearchFilter filter) {
            try {
                var users = await _service.AdvancedSearch(filter);

                  return Ok(new {
                    Status = 0,
                    Users = users
                });
            } catch (DomainException ex) {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.GetErrors));
            }
        }
    }
}