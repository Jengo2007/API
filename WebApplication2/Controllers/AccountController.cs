using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO;
using WebApplication2.Interfaces;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers;

public class AccountController : Controller
{
    public readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    // GET
    [HttpGet]
    public IActionResult Register()
    {
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
           return View(model);
        }
           
        var registerDto = new RegisterDto()
        {
            Username = model.Username,
            Email = model.Email,
            Password = model.Password 

        };
        var result =await _userService.Register(registerDto);
        if (!result)
        {
            ModelState.AddModelError("", "Ошибка регистрации");

            return View(model);
        }
        return RedirectToAction("Login");
    }
    
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var loginDto = new LoginDto
        {
            Username = model.Username,
            Password = model.Password
        };

        var token = await _userService.Login(loginDto);

        if (string.IsNullOrEmpty(token))
        {
            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims.ToList();

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal);
            
            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Ошибка авторизации");
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }
    
   
}