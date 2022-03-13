using AutoMapper;
using SmartManager.API.Response;
using SmartManager.Core.Exceptions;
using SmartManager.Domain.Entities;
using SmartManager.Infra.Interfaces;
using SmartManager.Infra.Repositories;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using SmartManager.Services.Requests;

namespace SmartManager.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository repository, IMapper mapper, ITokenService tokenService)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await _repository.GetByEmail(request.Email);

            if(user == null) {
                throw new DomainException("Usuário não encontrado com o email informado");
            }

            if(request.Password != user.Password) {
                throw new DomainException("Senha incorreta");
            }

            var token = _tokenService.GenerateToken(_mapper.Map<UserDTO>(request));


            return new AuthenticateResponse(0, token);

        }
    }
}