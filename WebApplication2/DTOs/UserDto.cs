using WebApplication2.Entities;

namespace WebApplication2.DTO;

public class UserDto
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Roles? Role { get; set; }
}