using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Persistence;

public class CashierContext:DbContext
{
    public CashierContext(DbContextOptions<CashierContext> options) : base(options)
    {

    }

    public DbSet<Cashiers> Cashiers { get; set; }

    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasOne<Cashiers>(c=>c.Cashier)
            .WithOne(u => u.User)
            .HasForeignKey<Cashiers>(u => u.UserId);
        modelBuilder.Entity<Admins>()
            .Property(a => a.AdminPassword)
            .IsRequired();
    }
    public DbSet<Admins> Admins { get; set; }
    
    
}

