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
    private readonly IFoodData _foodData;
    private readonly IOrderData _orderData;

    public OrdersController(IFoodData foodData, IOrderData orderData)
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
        var food = await _foodData.GetFood();
        OrderCreateModel model = new OrderCreateModel();
        
        food.ForEach(x =>
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

        var food = await _foodData.GetFood();

        order.Total = order.Quantity * food.Where(x => x.Id == order.FoodId).First().Price;

        int id = await _orderData.CreateOrder(order);

        return RedirectToAction("Display", new { id });
    }

    public async Task<IActionResult> Display(int id)
    {
        var displayOrder = new OrderDisplayModel();
        displayOrder.Order = await _orderData.GetOrderById(id);

        if (displayOrder.Order != null)
        {
            var food = await _foodData.GetFood();
            displayOrder.ItemPurchased = food.Where(x => x.Id == displayOrder.Order.FoodId).FirstOrDefault()?.Title;
        }

        return View(displayOrder);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, string orderName)
    {
        await _orderData.UpdateOrderName(id, orderName);
        return RedirectToAction("Display", new { id });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var order = await _orderData.GetOrderById(id);
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(OrderModel order)
    {
        await _orderData.DeleteOrder(order.Id);
        return RedirectToAction("Create");
    }
}