using WebApplication2.DTO;

namespace WebApplication2.Interfaces;

public interface IUserService
{
    Task<bool> Register(RegisterDto register);
    Task<string> Login(LoginDto login);
}