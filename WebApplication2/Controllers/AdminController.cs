using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DTO;
using WebApplication2.Entities;
using WebApplication2.Interfaces;
using WebApplication2.Persistence;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers;


    // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICashierRepository _cashierRepository;

        public AdminController(IUserRepository userRepository, ICashierRepository cashierRepository)
        {
            _userRepository = userRepository;
            _cashierRepository = cashierRepository;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _userRepository.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Cashiers()
        {
            var cashiers = await _cashierRepository.GetAllCashiers();
            return View(cashiers);
        }
        
        // Редактировать кассира — GET
        [HttpGet]
        public async Task<IActionResult> EditCashier(Guid id)
        {
            var cashier = await _cashierRepository.GetCashierById(id);
            if (cashier == null)
            {
                return NotFound();
            }

            var cashierDto = new CashierDto
            {
                CashierName = cashier.CashierName,
                CashierPhoneNumber = cashier.CashierPhoneNumber
            };

            ViewBag.CashierId = cashier.CashierId;
            return View(cashierDto);
        }

// Редактировать кассира — POST
        [HttpPost]
        public async Task<IActionResult> EditCashier(Guid id, CashierDto cashierDto)
        {
            if (!ModelState.IsValid)
            {
                return View(cashierDto);
            }

            await _cashierRepository.UpdateCashierById(cashierDto, id);
            return RedirectToAction("Cashiers");
        }

// Удалить кассира
        [HttpGet]
        public async Task<IActionResult> DeleteCashier(Guid id)
        {
            await _cashierRepository.DeleteCashierById(id);
            return RedirectToAction("Cashiers");
        }
        // [HttpGet]
        // public async Task<IActionResult> Index()
        // {
        //     // Получаем список кассиров напрямую
        //     var cashiers = await _context.Cashiers
        //         .Include(c => c.User) // При необходимости подгружаем данные из связанной сущности User
        //         .ToListAsync();
        //     
        //     // Получаем список обычных пользователей, фильтруя по роли "User"
        //     var users = await _context.Users
        //         .Where(u => u.Role.HasValue && u.Role.Value == Roles.User)
        //         .ToListAsync();
        //
        //     var viewModel = new AdminViewModel
        //     {
        //         Cashiers = cashiers,
        //         Users = users
        //     };
        //
        //     return View(viewModel);
        // }
        
        
    }

        
