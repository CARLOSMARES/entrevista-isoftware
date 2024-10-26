using Microsoft.EntityFrameworkCore;

namespace entrevista_isoftware
{
    public class MyDbContext : DbContext // Add inheritance from DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=Entrevista;User ID=sa;Password=Nachoc04042017@;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=azdata;Command Timeout=30");
            }
        }
    }
}
