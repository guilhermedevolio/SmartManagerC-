using AutoMapper;
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

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            var user = await _repository.GetByEmail(email);

            return _mapper.Map<List<UserDTO>>(user);
        }
    }
}