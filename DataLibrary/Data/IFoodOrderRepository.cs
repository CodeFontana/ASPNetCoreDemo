using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.Data;

public interface IFoodOrderRepository
{
    Task<IEnumerable<FoodOrderSummaryModel>> GetAllOrdersAsync();
    Task<FoodOrderModel?> GetOrderByIdAsync(int orderId);
    Task<int> CreateOrderAsync(FoodOrderModel order);
    Task<int> UpdateOrderNameAsync(int orderId, string orderName);
    Task<int> DeleteOrderAsync(int orderId);
}
