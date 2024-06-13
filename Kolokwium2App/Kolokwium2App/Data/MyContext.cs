using Microsoft.EntityFrameworkCore;

namespace Kolokwium2App.Data;

public class MyContext : DbContext
{
    protected MyContext()
    {
    }

    public MyContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}