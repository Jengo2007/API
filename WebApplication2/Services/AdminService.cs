using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using WebApplication2.DTO;
using WebApplication2.Entities;
using WebApplication2.Interfaces;
using WebApplication2.Persistence;

namespace WebApplication2.Services;
//
// public class AdminService: IAdminService
// {
//     private readonly ICashierRepository _cashierRepository;
//     private readonly CashierContext _cashierContext;
//     private readonly  PasswordHasher<Users> _passwordHasher;
//     public AdminService(ICashierRepository cashierRepository, CashierContext cashierContext,PasswordHasher<Users> passwordHasher)
//     {
//         _cashierRepository = cashierRepository;
//         _cashierContext = cashierContext;
//         _passwordHasher = passwordHasher;
//     }
//
//     public bool RegisterAdmin(RegisterAdminDto registerAdminDto)
//     {
//         if (string.IsNullOrEmpty(registerAdminDto.Password))
//             return false;
//         if (_cashierContext.Admins.Any(a => a.AdminName == registerAdminDto.Name))
//             return false;
//         var hashedPassword=_passwordHasher.HashPassword(null, registerAdminDto.Password);
//       
//         Users users = new()
//         {
//             Username = registerAdminDto.Name,
//             Password = registerAdminDto.Password,
//             Role = Roles.Admin
//         };
//         Admins admin = new()
//         {
//             AdminName = registerAdminDto.Name,
//             AdminPassword = hashedPassword,
//             AdminPhonenumber = registerAdminDto.PhoneNumber,
//             AdminId = users.Id,
//         };
//         _cashierContext.Admins.Add(admin);
//         _cashierContext.SaveChanges();
//
//         return true;
//     
//     }
//
//     public string Login(LoginDto loginDto)
//     {
//         throw new NotImplementedException();
//     }
// }