using ExpenseTracker.DTOs;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _service;

    public ExpensesController(IExpenseService service)
    {
        _service = service;
    }

    [HttpGet]
public async Task<IActionResult> GetAll(
    int? categoryId,
    DateTime? from,
    DateTime? to,
    string? sortBy,
    bool descending = false,
    int page = 1,
    int pageSize = 10)
{
    if (page < 1)
    {
        return BadRequest("Page must be greater than 0.");
    }

    if (pageSize < 1 || pageSize > 100)
    {
        return BadRequest("Page size must be between 1 and 100.");
    }

    var expenses = await _service.GetAllAsync(
        categoryId,
        from,
        to,
        sortBy,
        descending,
        page,
        pageSize);

    return Ok(expenses);
}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var expense = await _service.GetByIdAsync(id);

        if (expense is null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExpenseDto dto)
    {
        var expense = await _service.CreateAsync(dto);

        return Created($"/api/expenses/{expense.Id}", expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExpenseDto dto)
    {
        var expense = await _service.UpdateAsync(id, dto);

        if (expense is null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var summary = await _service.GetSummaryAsync();

        return Ok(summary);
    }
}