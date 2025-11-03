using GIFT_OF_THE_GIVERS_fOUNDATION.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace GIFT_OF_THE_GIVERS_fOUNDATION.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Volunteers> Volunteers { get; set; }
        public DbSet<Donors> Donors { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Donations> Donations { get; set; }
        public DbSet<GIFT_OF_THE_GIVERS_fOUNDATION.Models.Login> Login { get; set; } = default!;
        public DbSet<GIFT_OF_THE_GIVERS_fOUNDATION.Models.Register> Register { get; set; } = default!;

    }
}
    

