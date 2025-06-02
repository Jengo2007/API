using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels;

public class RegisterViewModel
{
    [Required]
    [MinLength(3)]
    public string? Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string? Password { get; set; } 
    [Required]
    [EmailAddress]
    [MinLength(5)]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Подтвердение пароля обязательно ")]
    [DataType(DataType.Password)]
    [Compare("Password",ErrorMessage = "Пароли не совпадают")]
    public string? ConfirmPassword { get; set; }    
}