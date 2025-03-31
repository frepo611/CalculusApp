using CalculusApp.Data;
using CalculusApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculusApp.Services;

public class DatabaseService
{
    private readonly AppDbContext _context;

    public DatabaseService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<ExpressionHistory>> GetExpressionHistoryAsync()
    {
        return await _context.ExpressionHistory.OrderByDescending(entry => entry.Timestamp)
            .ToListAsync();
            
    }
    public async Task AddExpressionHistoryAsync(ExpressionHistory history)
    {
        _context.ExpressionHistory.Add(history);
        await _context.SaveChangesAsync();
    }
}
