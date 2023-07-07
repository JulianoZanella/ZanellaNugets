namespace ApiBase.Services
{
    public interface ITokenService
    {
        string CreateToken(string userId);

        string GenerateRefreshToken();
    }
}
