using WebApplication2.DTO;

namespace WebApplication2.Interfaces;

public interface IUserService
{
    bool Register(RegisterDto register);
    string Login(LoginDto login);
}