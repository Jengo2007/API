using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities;

public class Admins
{
    [Key]
    public Guid AdminId { get; set; }
    [Required]
    [MaxLength(50)]
    public string? AdminName { get; set; }
    [Required]
    [MaxLength(50)]
    public string? AdminPassword { get; set; }
    [Required]
    [MaxLength(50)]
    public Int32 AdminPhonenumber { get; set; }
    [Required]
    [MaxLength(50)]
    public Roles? Role { get; set; }=Roles.Admin;
}