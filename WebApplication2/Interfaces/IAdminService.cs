using WebApplication2.DTO;

namespace WebApplication2.Interfaces;

public interface IAdminService
{
    public bool RegisterAdmin(RegisterAdminDto registerAdminDto);
    string Login(LoginDto loginDto);
    
}