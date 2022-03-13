using AutoMapper;
using Microsoft.Extensions.Configuration;
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

        private readonly IConfiguration _config;

        public AuthService(IUserRepository repository, IMapper mapper, ITokenService tokenService, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _config = config;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {

            var user = await _repository.GetByEmail(request.Email);

            if(user == null) {
                throw new DomainException("Usuário não encontrado com o email informado");
            }

            if(user.IsBlocked) {
                throw new DomainException("Conta bloqueada, tente novamente " + user.UnlockDate);
            }


            if(request.Password == user.Password) {
                var token = _tokenService.GenerateToken(user);
                return new AuthenticateResponse(0, token);
            }

            var attemps = user.AccessAttempts;
            var maxAttempts = Int32.Parse(_config.GetSection("Auth").GetSection("maxAttempts").Value);

            attemps++;
            user.changeAccessAttempts(attemps);

            if(attemps >= maxAttempts)
            {
                user.changeAccessAttempts(0);
                var expiresTime = DateTime.Now.AddMinutes(Double.Parse(_config.GetSection("Auth").GetSection("expiresMinutes").Value));
                user.changeUnlockDate(expiresTime);
            }

            await _repository.Update(user);

            if(user.IsBlocked) {
                throw new DomainException("Conta bloqueada, tente novamente " + user.UnlockDate);
            }
            
            throw new DomainException("Senha incorreta");

        }
    }
}