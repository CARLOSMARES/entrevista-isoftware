using Microsoft.EntityFrameworkCore;

namespace entrevista_isoftware
{
    public class MyDbContext : DbContext // Add inheritance from DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=Entrevista;User ID=sa;Password=Nachoc04042017@;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=azdata;Command Timeout=30");
        }
    }
}
