namespace MachinePortal.Data
{
    using Microsoft.EntityFrameworkCore;
    using MachinePortal.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<MachineEntry> MachineEntries { get; set; }
    }
}