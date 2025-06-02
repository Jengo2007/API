namespace WebApplication2.DTO;

public class CashierResponceDto
{
    public Guid CashierId { get; set; }
    public string CashierName { get; set; }
    public Int32 CashierPhoneNumber{get;set;}
    public Guid? UserId{get;set;}
}