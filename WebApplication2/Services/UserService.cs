using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.DTO;
using WebApplication2.Interfaces;
using WebApplication2.Persistence;
using WebApplication2.Entities;


namespace WebApplication2.Services;

public class UserService:IUserService
{
    
    private readonly CashierContext _context;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<User> _passwordHasher;
    
    public UserService(CashierContext context, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }
    public bool Register(RegisterDto register)
    {
        if (string.IsNullOrEmpty(register.Password))
            return false;
        if(_context.Users.Any(u=>u.Username == register.Username))
            return false;
        User user = new()
        {
            Username=register.Username,
            Password=register.Password
        };
        user.Password=_passwordHasher.HashPassword(user,register.Password);
        _context.Users.Add(user);
        _context.SaveChanges();
        return true;
        
        
    }

    public string Login(LoginDto login)
    { 
        var user = _context.Users.FirstOrDefault(u => u.Username == login.Username);
        if (user == null) return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);
        if (result == PasswordVerificationResult.Failed) return null;

        List<Claim> claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.Username),
            new Claim(ClaimTypes.Role,user.Role.ToString())    
        };

        // Используем правильный путь к ключу
        var keyString = _configuration["JwtSettings:Key"];
        if (string.IsNullOrEmpty(keyString))
            throw new Exception("JWT ключ не найден в конфигурации (JwtSettings:Key)");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpireMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    
    }
}