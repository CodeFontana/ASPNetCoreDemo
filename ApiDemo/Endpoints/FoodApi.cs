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

        app.MapPost("/api/food", CreateFoodAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireRateLimiting("fixed");

        app.MapPut("/api/food", UpdateFoodAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireRateLimiting("fixed");

        app.MapDelete("/api/food/{id}", DeleteFoodAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireRateLimiting("fixed");
    }

    private static async Task<IResult> ReadAllFoodAsync(IFoodRepository foodData)
    {
        IEnumerable<FoodModel> food = await foodData.GetFoodAsync();
        return Results.Ok(food);
    }

    private static async Task<IResult> CreateFoodAsync(IFoodRepository foodData, FoodModel food)
    {
        if (string.IsNullOrWhiteSpace(food.Title) || string.IsNullOrWhiteSpace(food.Description) || food.Price <= 0)
        {
            return Results.BadRequest("Title, Description, and a positive Price are required.");
        }

        int id = await foodData.CreateFoodAsync(food);
        return Results.Ok(new { Id = id });
    }

    private static async Task<IResult> UpdateFoodAsync(IFoodRepository foodData, FoodModel food)
    {
        if (food.Id <= 0 || string.IsNullOrWhiteSpace(food.Title) || string.IsNullOrWhiteSpace(food.Description) || food.Price <= 0)
        {
            return Results.BadRequest("A valid Id, Title, Description, and positive Price are required.");
        }

        await foodData.UpdateFoodAsync(food);
        return Results.Ok();
    }

    private static async Task<IResult> DeleteFoodAsync(IFoodRepository foodData, int id)
    {
        if (id <= 0)
        {
            return Results.BadRequest();
        }

        await foodData.DeleteFoodAsync(id);
        return Results.Ok();
    }
}
