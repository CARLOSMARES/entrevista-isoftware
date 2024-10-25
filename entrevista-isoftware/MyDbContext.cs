using Microsoft.EntityFrameworkCore;
using entrevista_isoftware;

namespace entrevista_isoftware;

public class MyDbContext
{

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

}
