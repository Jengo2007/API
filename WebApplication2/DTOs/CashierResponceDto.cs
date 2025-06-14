using WebApplication2.Entities;

namespace WebApplication2.DTO;

public class CashierResponceDto
{
    public Guid CashierId { get; set; }
    public string? CashierName { get; set; }
    public Int32 CashierPhoneNumber{get;set;}
    public Guid? UserId{get;set;}
    public string? Email { get; set; }
}