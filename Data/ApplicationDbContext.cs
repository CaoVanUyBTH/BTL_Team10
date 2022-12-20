using Nhom10.Models;
using Microsoft.EntityFrameworkCore;

namespace Nhom10.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
         public DbSet<Nhom10.Models.QLKH> QLKH { get; set; } = default!;
        public DbSet<Nhom10.Models.QLDH> QLDH { get; set; } = default!;
        public DbSet<Nhom10.Models.QLSP> QLSP { get; set; } = default!;
         public DbSet<Nhom10.Models.QLNV> QLNV { get; set; } = default!;
    }
}