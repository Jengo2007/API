using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using WebApplication2.Persistence;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers;


    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly CashierContext _context;

        public AdminController(CashierContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Получаем список кассиров напрямую
            var cashiers = await _context.Cashiers
                .Include(c => c.User) // При необходимости подгружаем данные из связанной сущности User
                .ToListAsync();
            
            // Получаем список обычных пользователей, фильтруя по роли "User"
            var users = await _context.Users
                .Where(u => u.Role.HasValue && u.Role.Value == Roles.User)
                .ToListAsync();

            var viewModel = new AdminViewModel
            {
                Cashiers = cashiers,
                Users = users
            };

            return View(viewModel);
        }
    }

        
