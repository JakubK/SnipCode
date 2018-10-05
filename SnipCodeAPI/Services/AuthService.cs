using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Services.Interfaces;
using System.Linq;

namespace SnipCodeAPI.Services
{
  public class AuthService : IAuthService
  {
    private IUserRepository userRepository;
    public AuthService(IUserRepository userRepositoryParam, ISeedService seedService)
    {
      this.userRepository = userRepositoryParam;
      seedService.Seed(userRepository);
    }
    public User Authenticate(LoginViewModel login)
    {
      return userRepository.GetUsers().FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
    }
  }
}