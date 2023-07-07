using ApiBase.DTOs;
using ApiBase.v1.DTOs;

namespace ApiBase.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;

        public AuthenticationService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public Task<ResultDTO<UserDTO>> GetCurrentUser(string userId)
        {
            // TODO
            return Task.FromResult(ResultDTO<UserDTO>.Success(CreateUserObject(userId)));
        }

        public Task<ResultDTO<UserDTO>> Login(LoginDTO loginDTO)
        {
            ResultDTO<UserDTO> result;
            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email))
            {
                result = ResultDTO<UserDTO>.Failure("Invalid Email");
            }
            else
            {
                // TODO
                result = ResultDTO<UserDTO>.Success(CreateUserObject(loginDTO.Email));
            }

            return Task.FromResult(result);
        }

        public Task<ResultDTO> Logout(string userId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<ResultDTO<UserDTO>> Register(RegisterDTO registerDTO)
        {
            // TODO
            return Task.FromResult(ResultDTO<UserDTO>.Success(CreateUserObject(registerDTO.Email ?? "5")));
        }

        private UserDTO CreateUserObject(string userId)
        {
            return new UserDTO
            {
                Token = _tokenService.CreateToken(userId),
            };
        }
    }
}
