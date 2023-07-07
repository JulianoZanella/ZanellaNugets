using ApiBase.DTOs;
using ApiBase.v1.DTOs;

namespace ApiBase.Services
{
    public interface IAuthenticationService
    {
        Task<ResultDTO<UserDTO>> Login(LoginDTO loginDTO);
        Task<ResultDTO> Logout(string userId);
        Task<ResultDTO<UserDTO>> Register(RegisterDTO registerDTO);
        Task<ResultDTO<UserDTO>> GetCurrentUser(string userId);
    }
}
