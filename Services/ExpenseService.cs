using ExpenseTracker.Data;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _context;

    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Expense>> GetAllAsync(
    int? categoryId,
    DateTime? from,
    DateTime? to,
    string? sortBy,
    bool descending,
    int page,
    int pageSize)
{
    var query = _context.Expenses
        .Include(e => e.Category)
        .AsQueryable();

    if (categoryId.HasValue)
        query = query.Where(e => e.CategoryId == categoryId.Value);

    if (from.HasValue)
        query = query.Where(e => e.Date >= from.Value);

    if (to.HasValue)
        query = query.Where(e => e.Date <= to.Value);

    query = sortBy?.ToLower() switch
    {
        "amount" => descending
            ? query.OrderByDescending(e => e.Amount)
            : query.OrderBy(e => e.Amount),

        "date" => descending
            ? query.OrderByDescending(e => e.Date)
            : query.OrderBy(e => e.Date),

        "description" => descending
            ? query.OrderByDescending(e => e.Description)
            : query.OrderBy(e => e.Description),

        _ => query.OrderBy(e => e.Date)
    };

    return await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
}

    public async Task<Expense?> GetByIdAsync(int id)
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Expense> CreateAsync(ExpenseDto dto)
    {
        var expense = new Expense
        {
            Amount = dto.Amount,
            Description = dto.Description,
            Date = dto.Date,
            CategoryId = dto.CategoryId
        };

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        return expense;
    }

    public async Task<Expense?> UpdateAsync(int id, ExpenseDto dto)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense is null)
            return null;

        expense.Amount = dto.Amount;
        expense.Description = dto.Description;
        expense.Date = dto.Date;
        expense.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();

        return expense;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense is null)
            return false;

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<ExpenseSummaryDto> GetSummaryAsync()
    {
        var totalAmount = await _context.Expenses.SumAsync(e => e.Amount);

        var expenseCount = await _context.Expenses.CountAsync();

        var averageAmount = expenseCount > 0
            ? await _context.Expenses.AverageAsync(e => e.Amount)
            : 0;

        var byCategory = await _context.Expenses
            .Include(e => e.Category)
            .GroupBy(e => new
            {
                e.CategoryId,
                CategoryName = e.Category!.Name
            })
            .Select(group => new CategorySummaryDto
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.CategoryName,
                TotalAmount = group.Sum(e => e.Amount)
            })
            .ToListAsync();

        return new ExpenseSummaryDto
        {
            TotalAmount = totalAmount,
            ExpenseCount = expenseCount,
            AverageAmount = averageAmount,
            ByCategory = byCategory
        };
    }
}