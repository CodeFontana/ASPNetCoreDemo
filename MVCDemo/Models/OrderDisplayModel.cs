using DataLibrary.Models;

namespace MVCDemo.Models;

public class OrderDisplayModel
{
    public OrderModel Order { get; set; } = new();
    public string? ItemPurchased { get; set; }
}
