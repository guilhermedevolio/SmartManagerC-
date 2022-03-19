using SmartManager.Services.DTOS;
using SmartManager.Services.Requests;

namespace SmartManager.Services.Interfaces
{
    public interface IUserService {
        Task<UserDTO> SearchByEmail(string email); 
        Task<List<UserDTO>> GetAllAsync(); 
        Task<UserDTO> CreateAsync(UserDTO userDTO); 
        Task<List<UserDTO>> AdvancedSearch(UserSearchFilter filter); 
    }
}