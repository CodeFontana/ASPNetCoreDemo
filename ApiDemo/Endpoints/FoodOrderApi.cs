using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ApiDemo.Endpoints;

public static class FoodOrderApi
{
    public static void AddFoodOrderApiEndpoints(this WebApplication app)
    {
        app.MapGet("/api/order", GetAllOrdersAsync)
            .Produces(StatusCodes.Status200OK)
            .RequireRateLimiting("fixed");

        app.MapPost("/api/order", CreateOrderAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireRateLimiting("fixed");
        
        app.MapGet("/api/order/{id}", GetOrderByIdAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireRateLimiting("fixed");

        app.MapDelete("/api/order/{id}", DeleteOrderAsync)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireRateLimiting("fixed");
    }

    private static async Task<IResult> GetAllOrdersAsync(IFoodOrderRepository orderData)
    {
        IEnumerable<FoodOrderSummaryModel> orders = await orderData.GetAllOrdersAsync();
        return Results.Ok(orders);
    }

    private static async Task<IResult> CreateOrderAsync(IFoodRepository foodData, IFoodOrderRepository orderData, FoodOrderModel order)
    {
        IEnumerable<FoodModel> food = await foodData.GetFoodAsync();
        order.Total = order.Quantity * food.Where(x => x.Id == order.FoodId).First().Price;
        int id = await orderData.CreateOrderAsync(order);
        return Results.Ok(new { Id = id });
    }
    
    private static async Task<IResult> GetOrderByIdAsync(IFoodRepository foodData, IFoodOrderRepository orderData, int id)
    {
        if (id == 0)
        {
            return Results.BadRequest();
        }
        
        FoodOrderModel? order = await orderData.GetOrderByIdAsync(id);
        
        if (order is not null)
        {
            IEnumerable<FoodModel> food = await foodData.GetFoodAsync();
            
            var output = new
            {
                Order = order,
                ItemPurchased = food
                    .Where(x => x.Id == order.FoodId)
                    .FirstOrDefault()?.Title
            };

            return Results.Ok(output);
        }

        return Results.NotFound();
    }

    private static async Task<IResult> DeleteOrderAsync(IFoodOrderRepository orderData, int id)
    {
        if (id <= 0)
        {
            return Results.BadRequest();
        }

        await orderData.DeleteOrderAsync(id);
        return Results.Ok();
    }
}
