using AutoMapper;
using SmartManager.Core.Exceptions;
using SmartManager.Domain.Entities;
using SmartManager.Infra.Interfaces;
using SmartManager.Infra.Repositories;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;

namespace SmartManager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDTO> SearchByEmail(string email)
        {
            var user = await _repository.GetByEmail(email);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDTO) {
            var userExists = await _repository.GetByEmail(userDTO.Email);

            if(userExists != null) {
                throw new DomainException("O email informado já existe");
            }

            var createUser = await _repository.Create(_mapper.Map<User>(userDTO));
            
            return _mapper.Map<UserDTO>(createUser);
        }
    }
}