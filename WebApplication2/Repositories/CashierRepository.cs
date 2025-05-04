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
    public Cashiers AddCashier(Cashiers cashier)
    {
        var newCashier = new Cashiers()
        {
            CashierName = cashier.CashierName,
            CashierPhoneNumber = cashier.CashierPhoneNumber
        };
        _context.Cashiers.Add(newCashier);
        _context.SaveChanges(); 
        return newCashier; 
    }

    public List<Cashiers> GetAllCashiers()
    {
        return _context.Cashiers.ToList();
    }
    

    public Cashiers UpdateCashierById(CashierDto cashier, Guid id)
    {

        var cashierById = _context.Cashiers.FirstOrDefault(c => c.CashierID == id);
        if (cashierById != null)
        {
            cashierById.CashierName = cashier.CashierName;
            cashierById.CashierPhoneNumber = cashier.CashierPhoneNumber;
            _context.Cashiers.Update(cashierById);
            _context.SaveChanges();
        }
        return cashierById;    }

    public Cashiers DeleteCashierById(Guid id)
    {
        
        var cashierToDelete = _context.Cashiers.FirstOrDefault(c => c.CashierID == id);
        if (cashierToDelete != null)
        {
            _context.Cashiers.Remove(cashierToDelete);
            _context.SaveChanges();
        }
        return cashierToDelete;
    }

    public Cashiers? GetCashierById(Guid id)
    {
        return _context.Cashiers.FirstOrDefault(c => c.UserId == id);

    }
}