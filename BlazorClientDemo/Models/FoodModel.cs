namespace BlazorClientDemo.Models;

public sealed class FoodModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
