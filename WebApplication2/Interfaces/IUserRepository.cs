using DocumentFormat.OpenXml.Spreadsheet;
using WebApplication2.DTO;
using WebApplication2.Entities;

namespace WebApplication2.Interfaces;

public interface IUserRepository
{
    public User AddUser(UserDto userDto);
    public List<User> GetAllUsers();
    public User GetUserById(Guid id);
    public User DeleteUserById(Guid id);
    public User UpdateUser(UserDto user,Guid id);
    
}