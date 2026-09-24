namespace ExpenseTracker.DTOs;

public class ExpenseSummaryDto
{
    public decimal TotalAmount { get; set; }

    public int ExpenseCount { get; set; }

    public decimal AverageAmount { get; set; }

    public List<CategorySummaryDto> ByCategory { get; set; } = [];
}

public class CategorySummaryDto
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = "";

    public decimal TotalAmount { get; set; }
}