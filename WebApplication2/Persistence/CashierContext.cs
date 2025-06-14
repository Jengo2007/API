using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Persistence;

public class CashierContext:DbContext
{
    public CashierContext(DbContextOptions<CashierContext> options) : base(options)
    {
        
    }

    public DbSet<Cashier> Cashiers { get; set; }

    
    public DbSet<User> Users { get; set; }
    public DbSet<Admins> Admins { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasOne<Cashier>(c=>c.Cashier)
            .WithOne(u => u.User)
            .HasForeignKey<Cashier>(u => u.UserId);
        modelBuilder.Entity<Admins>()
            .Property(a => a.AdminPassword)
            .IsRequired();
        // Добавим пользователей
        var user1Id = Guid.NewGuid();
        var user2Id = Guid.NewGuid();

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = user1Id,
                Username = "admin",
                Password = "hashed_password_admin",
                Email = "admin@mail.com",
                Role = Roles.Admin
            },
            new User
            {
                Id = user2Id,
                Username = "cashier1",
                Password = "hashed_password_cashier",
                Email = "cashier@mail.com",
                Role = Roles.Cashier
            }
        );

        // Добавим кассира
        modelBuilder.Entity<Cashier>().HasData(
            new Cashier
            {
                CashierID = Guid.NewGuid(),
                CashierName = "Иван Иванов",
                CashierPhoneNumber = 900123456,
                UserId = user2Id
            }
        );
    }
    }
    
    
    


