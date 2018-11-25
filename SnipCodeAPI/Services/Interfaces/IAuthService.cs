using SnipCodeAPI.Models;

namespace SnipCodeAPI.Services.Interfaces
{
    public interface IAuthService
    {
        JsonWebToken Authenticate(LoginViewModel login);
    }
}