using SmartManager.Services.DTOS;

namespace SmartManager.Services.Interfaces
{
    public interface IUserService {
        Task<List<UserDTO>> SearchByEmail(string email); 
    }
}