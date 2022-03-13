using SmartManager.Services.DTOS;

namespace SmartManager.Services.Interfaces
{
    public interface IUserService {
        Task<UserDTO> SearchByEmail(string email); 
    }
}