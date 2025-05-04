using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using WebApplication2.DTO;
using WebApplication2.Entities;
using WebApplication2.Interfaces;
using WebApplication2.Persistence;
namespace WebApplication2.Repositories;

public class UserRepository:IUserRepository
{
    private readonly CashierContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserRepository(CashierContext context,IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }


    public User AddUser(UserDto userDto)
    {

        var newUser = new User
        {
            Username = userDto.Username,
            Password = userDto.Password,
           
        };
        if (userDto.Password != null) newUser.Password = _passwordHasher.HashPassword(newUser, userDto.Password);
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public User GetUserById(Guid id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);

    }

    public User DeleteUserById(Guid id)
    {
        
        var userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);
        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
        return userToDelete;
    }

    public User UpdateUser(UserDto user, Guid id)
    {
        var userById = _context.Users.FirstOrDefault(u => u.Id == id);
        if (userById != null)
        {
            userById.Username = user.Username;
            userById.Password = user.Password;
            _context.Users.Update(userById);
            _context.SaveChanges();
        }
        return userById;  
    }
}