using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ExpenseTracker.Tests;

public class ExpenseApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ExpenseApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetExpenses_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/api/expenses");

        Assert.True(response.IsSuccessStatusCode);
    }
}