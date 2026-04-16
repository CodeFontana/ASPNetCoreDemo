namespace BlazorClientDemo.Models;

public sealed class FoodOrderSummaryModel
{
    public int Id { get; set; }
    public string? OrderName { get; set; }
    public DateTime OrderDate { get; set; }
    public int FoodId { get; set; }
    public string? FoodTitle { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}
