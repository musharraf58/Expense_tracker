using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private static readonly List<Expense> expenses = new()
    {
        new Expense
        {
            Id = 1,
            Amount = 25.50m,
            Description = "Lunch",
            Date = DateTime.Now
        },
        new Expense
        {
            Id = 2,
            Amount = 10.00m,
            Description = "Coffee",
            Date = DateTime.Now
        }
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == id);

        if (expense is null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    public IActionResult Create(Expense expense)
    {
        expenses.Add(expense);

        return Created($"/api/expenses/{expense.Id}", expense);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Expense updatedExpense)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == id);

        if (expense is null)
        {
            return NotFound();
        }

        expense.Amount = updatedExpense.Amount;
        expense.Description = updatedExpense.Description;
        expense.Date = updatedExpense.Date;

        return Ok(expense);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == id);

        if (expense is null)
        {
            return NotFound();
        }

        expenses.Remove(expense);

        return NoContent();
    }
}