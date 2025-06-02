using DocumentFormat.OpenXml.Spreadsheet;
using WebApplication2.DTO;
using WebApplication2.Entities;

namespace WebApplication2.Interfaces;

public interface IUserRepository
{
    Task<User> AddUser(UserDto userDto);
    Task< List<User>> GetAllUsers();
    Task<User> GetUserById(Guid id); 
    Task<User> DeleteUserById(Guid id);
    Task< User> UpdateUser(UserDto user,Guid id);
    
}