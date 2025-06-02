using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities;

public class Cashier
{
    [Key]
    [Column("CashierID")]
    public Guid CashierID { get; set; }
    [MaxLength(50)]
    [Column("CashierName")]
    public string? CashierName { get; set; }
    [Column("CashierPhoneNumber")]
    public Int32 CashierPhoneNumber { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}