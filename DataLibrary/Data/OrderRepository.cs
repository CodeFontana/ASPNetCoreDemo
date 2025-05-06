using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace DataLibrary.Data;

public class OrderRepository : IOrderRepository
{
    private readonly IDataAccess _db;
    private readonly string _connectionString;

    public OrderRepository(IDataAccess dataAccess, IConfiguration config)
    {
        _db = dataAccess;
        _connectionString = config.GetConnectionString("Default")
            ?? throw new Exception("Connection string 'Default' missing from configuration");
    }

    public async Task<OrderModel?> GetOrderByIdAsync(int orderId)
    {
        OrderModel? result = await _db.QueryFirstOrDefaultAsync<OrderModel, dynamic>(
            "dbo.spOrders_GetById",
            new
            {
                Id = orderId
            },
            _connectionString);

        return result;
    }

    public async Task<int> CreateOrderAsync(OrderModel order)
    {
        DynamicParameters parameters = new();
        parameters.Add("OrderName", order.OrderName);
        parameters.Add("OrderDate", order.OrderDate);
        parameters.Add("FoodId", order.FoodId);
        parameters.Add("Quantity", order.Quantity);
        parameters.Add("Total", order.Total);
        parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

        await _db.ExecuteAsync(
            "dbo.spOrders_Insert", 
            parameters, 
            _connectionString);

        return parameters.Get<int>("Id");
    }

    public Task<int> UpdateOrderNameAsync(int orderId, string orderName)
    {
        return _db.ExecuteAsync("dbo.spOrders_UpdateName",
                                    new
                                    {
                                        Id = orderId,
                                        OrderName = orderName
                                    },
                                    _connectionString);
    }

    public Task<int> DeleteOrderAsync(int orderId)
    {
        return _db.ExecuteAsync("dbo.spOrders_Delete",
                                    new
                                    {
                                        Id = orderId
                                    },
                                    _connectionString);
    }
}