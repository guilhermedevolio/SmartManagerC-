using System.Security.Cryptography.X509Certificates;
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
        private readonly IRefreshTokenRepository _TokenRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repository, IMapper mapper, ITokenService tokenService, IConfiguration config, IRefreshTokenRepository tokenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _config = config;
            _TokenRepository = tokenRepository;
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

                var refreshEntityToken = new Entities.RefreshToken{
                    Token = _tokenService.GenerateRandomToken(32),
                    Expires = DateTime.UtcNow.AddHours(10),
                    UserId = user.Id
                };

                var refreshToken = await _TokenRepository.Create(refreshEntityToken);
     
                return new AuthenticateResponse(0, token, refreshToken.Token);
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

        public Boolean ValidateToken(string token) {
            var validate = _tokenService.ValidateToken(token);

            if(!validate) {
                throw new Exception("O token informado é inválido");
            }

            return true;
        }

        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest tokenPayload) {
            var token = tokenPayload.Token;

            var refreshToken = await _TokenRepository.getToken(token);

            if(refreshToken == null) {
                throw new DomainException("O Token informado não existe");
            }

            if(refreshToken.IsActive){
                refreshToken.Revoked = DateTime.UtcNow;
                await _TokenRepository.Update(refreshToken);

                var jwtToken = _tokenService.GenerateToken(refreshToken.User);

                return new RefreshTokenResponse() {
                    RevokedToken = refreshToken.Token,
                    AccessToken = jwtToken
                };
            }
                
            throw new DomainException("O Token informado não é mais válido!");
        }
    }
}