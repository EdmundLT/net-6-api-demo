using Microsoft.EntityFrameworkCore;

namespace APIDemo.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    public DbSet<SuperHero> Super { get; set; }
}