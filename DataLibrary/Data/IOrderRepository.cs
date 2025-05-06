using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.Data;

public interface IOrderRepository
{
    Task<OrderModel?> GetOrderByIdAsync(int orderId);
    Task<int> CreateOrderAsync(OrderModel order);
    Task<int> UpdateOrderNameAsync(int orderId, string orderName);
    Task<int> DeleteOrderAsync(int orderId);
}