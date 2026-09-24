using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs;

public class ExpenseDto
{
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = "";

    public DateTime Date { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}