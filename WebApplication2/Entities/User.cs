using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]  
    [MaxLength(50)]
    public string? Username { get; set; }
    [Required]
    [MaxLength(256)]
    public string? Password { get; set; }
    public Cashiers? Cashier { get; set; }
    public Roles? Role { get; set; } = Roles.User;


}