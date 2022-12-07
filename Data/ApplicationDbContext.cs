using Nhom10.Models;
using Microsoft.EntityFrameworkCore;

namespace Nhom10.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set;}
        public DbSet<Customer> Customers { get; set;}
        public DbSet<Nhom10.Models.QLDH> QLDH { get; set; } = default!;
        public DbSet<Nhom10.Models.QLDH> QLSP { get; set; } = default!;
    }
}