using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ApiDemo.Endpoints;

public static class FoodApi
{
    public static void AddFoodApiEndpoints(this WebApplication app)
    {
        app.MapGet("/api/food", ReadAllFoodAsync)
            .Produces(StatusCodes.Status200OK)
            .RequireRateLimiting("fixed");
    }

    private static async Task<IResult> ReadAllFoodAsync(IFoodRepository foodData)
    {
        IEnumerable<FoodModel> food = await foodData.GetFoodAsync();
        return Results.Ok(food);
    }
}
