using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace DataLibrary.Data;

public sealed class FoodRepository : IFoodRepository
{
    private readonly IDataAccess _db;
    private readonly string _connectionString;

    public FoodRepository(IDataAccess dataAccess, IConfiguration config)
    {
        _db = dataAccess;
        _connectionString = config.GetConnectionString("Default") 
            ?? throw new Exception("Connection string 'Default' missing from configuration");
    }

    public async Task<IEnumerable<FoodModel>> GetFoodAsync()
    {
        return await _db.QueryMultipleAsync<FoodModel>(
            "dbo.spFood_GetAll",
            _connectionString);
    }

    public async Task<int> CreateFoodAsync(FoodModel food)
    {
        DynamicParameters parameters = new();
        parameters.Add("pTitle", food.Title);
        parameters.Add("pDescription", food.Description);
        parameters.Add("pPrice", food.Price);
        parameters.Add("oId", DbType.Int32, direction: ParameterDirection.Output);

        await _db.ExecuteAsync(
            "dbo.spFood_Insert",
            parameters,
            _connectionString);

        return parameters.Get<int>("oId");
    }

    public Task<int> UpdateFoodAsync(FoodModel food)
    {
        return _db.ExecuteAsync(
            "dbo.spFood_Update",
            new
            {
                pId = food.Id,
                pTitle = food.Title,
                pDescription = food.Description,
                pPrice = food.Price
            },
            _connectionString);
    }

    public Task<int> DeleteFoodAsync(int foodId)
    {
        return _db.ExecuteAsync(
            "dbo.spFood_Delete",
            new { pId = foodId },
            _connectionString);
    }
}
