using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace DataLibrary.Data;

public sealed class FoodOrderRepository : IFoodOrderRepository
{
    private readonly IDataAccess _db;
    private readonly string _connectionString;

    public FoodOrderRepository(IDataAccess dataAccess, IConfiguration config)
    {
        _db = dataAccess;
        _connectionString = config.GetConnectionString("Default")
            ?? throw new Exception("Connection string 'Default' missing from configuration");
    }

    public async Task<IEnumerable<FoodOrderSummaryModel>> GetAllOrdersAsync()
    {
        return await _db.QueryMultipleAsync<FoodOrderSummaryModel>(
            "dbo.spFoodOrders_GetAll",
            _connectionString);
    }

    public async Task<FoodOrderModel?> GetOrderByIdAsync(int orderId)
    {
        FoodOrderModel? result = await _db.QueryFirstOrDefaultAsync<FoodOrderModel, dynamic>(
            "dbo.spFoodOrders_GetById",
            new { pId = orderId },
            _connectionString);

        return result;
    }

    public async Task<int> CreateOrderAsync(FoodOrderModel order)
    {
        DynamicParameters parameters = new();
        parameters.Add("pOrderName", order.OrderName);
        parameters.Add("pOrderDate", order.OrderDate);
        parameters.Add("pFoodId", order.FoodId);
        parameters.Add("pQuantity", order.Quantity);
        parameters.Add("pTotal", order.Total);
        parameters.Add("oId", DbType.Int32, direction: ParameterDirection.Output);

        await _db.ExecuteAsync(
            "dbo.spFoodOrders_Insert", 
            parameters, 
            _connectionString);

        return parameters.Get<int>("oId");
    }

    public Task<int> UpdateOrderNameAsync(int orderId, string orderName)
    {
        return _db.ExecuteAsync("dbo.spFoodOrders_UpdateName",
                                    new
                                    {
                                        pId = orderId,
                                        pOrderName = orderName
                                    },
                                    _connectionString);
    }

    public Task<int> DeleteOrderAsync(int orderId)
    {
        return _db.ExecuteAsync("dbo.spFoodOrders_Delete",
                                    new
                                    {
                                        pId = orderId
                                    },
                                    _connectionString);
    }
}
