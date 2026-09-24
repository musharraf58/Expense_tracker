using ExpenseTracker.Data;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExpensesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int? categoryId)
    {
        var query = _context.Expenses
            .Include(e => e.Category)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(e => e.CategoryId == categoryId.Value);
        }

        var expenses = await query.ToListAsync();

        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var expense = await _context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (expense is null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExpenseDto dto)
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

        return Created($"/api/expenses/{expense.Id}", expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExpenseDto dto)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense is null)
        {
            return NotFound();
        }

        expense.Amount = dto.Amount;
        expense.Description = dto.Description;
        expense.Date = dto.Date;
        expense.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();

        return Ok(expense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense is null)
        {
            return NotFound();
        }

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}