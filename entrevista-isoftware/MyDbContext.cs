using Microsoft.EntityFrameworkCore;

namespace entrevista_isoftware
{
    public class MyDbContext : DbContext // Add inheritance from DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1\sqlserver;Initial Catalog=master;DataBase=prueba;User ID=sa;Password=Nachoc04042017@;Trusted_Connection=True;");
        }
    }
}
