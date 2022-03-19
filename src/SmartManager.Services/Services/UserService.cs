using AutoMapper;
using LinqKit;
using Microsoft.IdentityModel.Tokens;
using SmartManager.Core.Exceptions;
using SmartManager.Domain.Entities;
using SmartManager.Infra.Interfaces;
using SmartManager.Infra.Repositories;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using SmartManager.Services.Requests;

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

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _repository.Get();

            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<List<UserDTO>> AdvancedSearch(UserSearchFilter filter)
        {
            var pb = PredicateBuilder.New<User>(true);

            if(!filter.Name.IsNullOrEmpty()) {
                pb = pb.And(p => p.Name == filter.Name);
            }

            if(!filter.Email.IsNullOrEmpty()) {
                pb = pb.And(p => p.Email == filter.Email);
            }

            var search = await _repository.AdvancedSearch(pb);

            return  _mapper.Map<List<UserDTO>>(search);
        }
    }
}