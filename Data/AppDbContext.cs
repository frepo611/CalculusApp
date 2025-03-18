using CalculusApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculusApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<ExpressionHistory> ExpressionHistory { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configuration if needed
    }
}
