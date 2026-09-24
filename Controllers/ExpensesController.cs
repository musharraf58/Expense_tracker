using ExpenseTracker.Data;
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
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _context.Expenses.ToListAsync();

        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense is null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        return Created($"/api/expenses/{expense.Id}", expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Expense updatedExpense)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense is null)
        {
            return NotFound();
        }

        expense.Amount = updatedExpense.Amount;
        expense.Description = updatedExpense.Description;
        expense.Date = updatedExpense.Date;

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