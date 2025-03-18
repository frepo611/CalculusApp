using Microsoft.EntityFrameworkCore.Design;
using CalculusApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CalculusApp.Services;

public class ExpressionHistoryContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expressions.db");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite($"Filename={dbPath}");
        return new AppDbContext(optionsBuilder.Options);
    }
}
