using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
}