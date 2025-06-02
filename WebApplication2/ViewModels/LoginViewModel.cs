using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels;

public class LoginViewModel
{
    [Required]
    [MinLength(3)]
    public string? Username { get; set; }
    [Required]
    [MinLength(3)]
    public string? Password { get; set; }
}