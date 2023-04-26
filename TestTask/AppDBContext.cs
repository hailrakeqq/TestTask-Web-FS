using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions options) : base(options) { }

    public DbSet<Catalog> catalogs { get; set; }
}
