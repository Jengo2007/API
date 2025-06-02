using OfficeOpenXml;
using WebApplication2.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using WebApplication2.DTO;
using WebApplication2.Entities;
using WebApplication2.Persistence;


namespace WebApplication2.Services;

public class CashierService:ICashierService
{
    private readonly ICashierRepository _cashierRepository;
    private readonly CashierContext _cashierContext;
    private readonly  PasswordHasher<User> _passwordHasher;
    public CashierService(ICashierRepository cashierRepository, CashierContext cashierContext,PasswordHasher<User> passwordHasher)
    {
        _cashierRepository = cashierRepository;
        _cashierContext = cashierContext;
        _passwordHasher = passwordHasher;
    }
    public async Task<byte[]> GenerateCasiersFile()
    {

        var allCashiers = await _cashierRepository.GetAllCashiers();
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Cashiers");
        worksheet.Cell(1,1).Value = "Name";
        worksheet.Cell(1,2).Value = "Phone Number";
        for(var i = 0; i < allCashiers.Count; i++)
        {
            worksheet.Cell(i + 2,1).Value = allCashiers[i].CashierName;
            worksheet.Cell(i + 2,2).Value = allCashiers[i].CashierPhoneNumber;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<bool> RegisterCashier(RegisterCashierDto registerCashierDto)
    {
        if (string.IsNullOrEmpty(registerCashierDto.Password))
            return false;
        if (_cashierContext.Cashiers.Any(c => c.CashierName == registerCashierDto.Name))
            return false;
        var hashedPassword=_passwordHasher.HashPassword(null, registerCashierDto.Password);
        User user = new()
        {
            Username = registerCashierDto.Username,
            Password = hashedPassword,
            Role = Roles.Cashier
        };
        _cashierContext.Users.Add(user);
         await _cashierContext.SaveChangesAsync();

        Cashier cashier = new()
        {
            CashierName = registerCashierDto.Name,
            CashierPhoneNumber = registerCashierDto.PhoneNumber,
            UserId = user.Id
        };
        _cashierContext.Cashiers.Add(cashier);
         await _cashierContext.SaveChangesAsync();

        return true;
    }
    
}