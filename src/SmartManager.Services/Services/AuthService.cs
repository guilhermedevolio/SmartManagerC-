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
            var UserDTO = _mapper.Map<User>(request);

            var user = await _repository.GetByEmail(UserDTO.Email);

            if(user == null) {
                throw new DomainException("Usuário não encontrado com o email informado");
            }

            if(user.IsBlocked) {
                throw new DomainException("Conta bloqueada, tente novamente em alguns minutos");
            }

            var attemps = user.AccessAttempts;

            if(request.Password == user.Password) {
                var token = _tokenService.GenerateToken(_mapper.Map<UserDTO>(request));

                return new AuthenticateResponse(0, token);
            }

            attemps++;
            // Adiciona uma tentativa de erro
            user.changeAccessAttempts(attemps);

            if(attemps >= 3){
                user.changeAccessAttempts(0);

                if(!user.IsBlocked) {
                    user.changeUnlockDate(DateTime.Now.AddMinutes(10));
                }

                await _repository.Update(user);
                throw new DomainException("Conta bloqueada, tente novamente em alguns minutos" + DateTime.Now);
            }

            await _repository.Update(user);
            
            throw new DomainException("Senha incorreta");

        }
    }
}