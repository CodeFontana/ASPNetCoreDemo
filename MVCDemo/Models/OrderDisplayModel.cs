using DataLibrary.Models;

namespace MVCDemo.Models;

public sealed class OrderDisplayModel
{
    public OrderModel? Order { get; set; }
    public string? ItemPurchased { get; set; }
}
