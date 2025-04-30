using dotnet_cp4.Persistence.Models;
using Microsoft.EntityFrameworkCore;


namespace dotnet_cp4.Persistence
{
    public class FIAPDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public FIAPDbContext(DbContextOptions<FIAPDbContext> options) : base(options)
        {

        }
    }
}
