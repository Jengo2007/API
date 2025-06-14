using Microsoft.EntityFrameworkCore;
using WebApplication2.DTO;
using WebApplication2.Entities;
using WebApplication2.Interfaces;
using WebApplication2.Persistence;

namespace WebApplication2.Repositories;

public class CashierRepository:ICashierRepository
{
    private readonly CashierContext _context;

    public CashierRepository(CashierContext context)
    {
        _context = context;
    }
    public async Task<Cashier> AddCashier(Cashier cashier)
    {
        var newCashier = new Cashier()
        {
            CashierName = cashier.CashierName,
            CashierPhoneNumber = cashier.CashierPhoneNumber
        };
        _context.Cashiers.Add(newCashier);
        await _context.SaveChangesAsync(); 
        return newCashier; 
    }

    public async Task<List<CashierResponceDto>> GetAllCashiers()
    {
        var cashiers = await _context.Cashiers
            .Include(c => c.User)
            .Select(c => new CashierResponceDto()
            {
                CashierId = c.CashierID,
                CashierName = c.CashierName,
                CashierPhoneNumber = c.CashierPhoneNumber,
                UserId = c.UserId,
                Email = c.User.Email,
            })
            .ToListAsync();

        return cashiers;
    }
    

    public async Task<Cashier> UpdateCashierById(CashierDto cashier, Guid id)
    {

        var cashierById =await _context.Cashiers.FirstOrDefaultAsync(c => c.CashierID == id);
        if (cashierById != null)
        {
            cashierById.CashierName = cashier.CashierName;
            cashierById.CashierPhoneNumber = cashier.CashierPhoneNumber;
            _context.Cashiers.Update(cashierById);
            await _context.SaveChangesAsync();
        }
        return cashierById;    
    }

    public async Task<Cashier> DeleteCashierById(Guid id)
    {
        
        var cashierToDelete = await _context.Cashiers.FirstOrDefaultAsync(c => c.CashierID == id);
        if (cashierToDelete != null)
        {
            _context.Cashiers.Remove(cashierToDelete);
            await _context.SaveChangesAsync();
        }
        return cashierToDelete;
    }

    public async Task<CashierResponceDto?> GetCashierById(Guid id)
    {
        return await _context.Cashiers.Select(c=>new CashierResponceDto()
        {
            CashierId = c.CashierID,
            CashierName = c.CashierName,
            CashierPhoneNumber = c.CashierPhoneNumber,
            UserId = c.UserId
        }).FirstOrDefaultAsync(c => c.CashierId == id);

    }
}