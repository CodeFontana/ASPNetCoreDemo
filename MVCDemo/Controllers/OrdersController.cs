using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemo.Models;

namespace MVCDemo.Controllers;

public class OrdersController : Controller
{
    private readonly IFoodRepository _foodData;
    private readonly IOrderRepository _orderData;

    public OrdersController(IFoodRepository foodData, IOrderRepository orderData)
    {
        _foodData = foodData;
        _orderData = orderData;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Create()
    {
        IEnumerable<FoodModel> food = await _foodData.GetFoodAsync();
        OrderCreateModel model = new();

        food.ToList().ForEach(x =>
        {
            model.FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
        });

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderModel order)
    {
        if (ModelState.IsValid == false)
        {
            return View();
        }

        IEnumerable<FoodModel> food = await _foodData.GetFoodAsync();
        order.Total = order.Quantity * food.Where(x => x.Id == order.FoodId).First().Price;
        int id = await _orderData.CreateOrderAsync(order);

        return RedirectToAction("Display", new { id });
    }

    public async Task<IActionResult> Display(int id)
    {
        OrderDisplayModel displayOrder = new()
        {
            Order = await _orderData.GetOrderByIdAsync(id)
        };

        if (displayOrder.Order != null)
        {
            IEnumerable<FoodModel> food = await _foodData.GetFoodAsync();
            displayOrder.ItemPurchased = food
                .Where(x => x.Id == displayOrder.Order.FoodId)
                .FirstOrDefault()?
                .Title ?? throw new InvalidOperationException("Invalid order - FoodId is not found");
        }

        return View(displayOrder);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, string orderName)
    {
        await _orderData.UpdateOrderNameAsync(id, orderName);
        return RedirectToAction("Display", new { id });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var order = await _orderData.GetOrderByIdAsync(id);
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(OrderModel order)
    {
        await _orderData.DeleteOrderAsync(order.Id);
        return RedirectToAction("Create");
    }
}