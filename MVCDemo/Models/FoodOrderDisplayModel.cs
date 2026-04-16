using DataLibrary.Models;

namespace MVCDemo.Models;

public sealed class FoodOrderDisplayModel
{
    public FoodOrderModel? Order { get; set; }
    public string? ItemPurchased { get; set; }
}
