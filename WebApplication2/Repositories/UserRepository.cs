using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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


    public async Task<User> AddUser(UserDto userDto)
    {

        var newUser = new User
        {
            Username = userDto.Username,
            Password = userDto.Password,
           
        };
        if (userDto.Password != null) newUser.Password = _passwordHasher.HashPassword(newUser, userDto.Password);
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return newUser;
    } 

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserById(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    }

    public async Task<User> DeleteUserById(Guid id)
    {
        
        var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
        return userToDelete;
    }

    public async Task<User> UpdateUser(UserDto user, Guid id)
    {
        var userById = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (userById != null)
        {
            userById.Username = user.Username;
            userById.Password = user.Password;
            _context.Users.Update(userById); 
            await _context.SaveChangesAsync();
        }
        return userById;  
    }
}