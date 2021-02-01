using Dapper;
using DataLibrary.Db;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class OrderData : IOrderData
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public OrderData(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateOrder(OrderModel order)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("OrderName", order.OrderName);
            p.Add("OrderDate", order.OrderDate);
            p.Add("FoodId", order.FoodId);
            p.Add("Quantity", order.Quantity);
            p.Add("Total", order.Total);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spOrders_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> UpdateOrderName(int orderId, string orderName)
        {
            return _dataAccess.SaveData("dbo.spOrders_UpdateName",
                                        new
                                        {
                                            Id = orderId,
                                            OrderName = orderName
                                        },
                                        _connectionString.SqlConnectionName);
        }

        public Task<int> DeleteOrder(int orderId)
        {
            return _dataAccess.SaveData("dbo.spOrders_Delete",
                                        new
                                        {
                                            Id = orderId
                                        },
                                        _connectionString.SqlConnectionName);
        }

        public async Task<OrderModel> GetOrderById(int orderId)
        {
            List<OrderModel> recs = await _dataAccess.LoadData<OrderModel, dynamic>(
                "dbo.spOrders_GetById",
                new
                {
                    Id = orderId
                },
                _connectionString.SqlConnectionName);

            return recs.FirstOrDefault();
        }
    }
}