using Microsoft.EntityFrameworkCore;

using BTPTPMQL.Models;

namespace BTPTPMQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Person> Person { get; set; }
        public DbSet<HeThongPhanPhoi> HeThongPhanPhoi { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<DaiLy> DaiLy { get; set; }
    }
}