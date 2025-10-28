using melkikerapostgrescrud.Entities;
using Microsoft.EntityFrameworkCore;

namespace melkikerapostgrescrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ArretsLigne> ArretsLigne { get; set; }
        public DbSet<Pointgeo> PointGeos { get; set; }
    }
}
