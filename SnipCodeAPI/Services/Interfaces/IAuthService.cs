using SnipCodeAPI.Models;
using SnipCodeAPI.Models.Requests;

namespace SnipCodeAPI.Services.Interfaces
{
    public interface IAuthService
    {
        JsonWebToken Authenticate(LoginViewModel login);
        bool TryChangePassword(string email,ChangePasswordRequest changePasswordRequest);
    }
}