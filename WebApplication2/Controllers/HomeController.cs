using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}