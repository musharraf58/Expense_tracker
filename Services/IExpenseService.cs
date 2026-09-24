using ExpenseTracker.DTOs;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services;

public interface IExpenseService
{
    Task<List<Expense>> GetAllAsync(
        int? categoryId,
        DateTime? from,
        DateTime? to,
        string? sortBy,
        bool descending,
        int page,
        int pageSize);

    Task<Expense?> GetByIdAsync(int id);

    Task<Expense> CreateAsync(ExpenseDto dto);

    Task<Expense?> UpdateAsync(int id, ExpenseDto dto);

    Task<bool> DeleteAsync(int id);

    Task<ExpenseSummaryDto> GetSummaryAsync();
}