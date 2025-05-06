using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RPDemo.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly IFoodRepository _foodData;
    private readonly IOrderRepository _orderData;

    public CreateModel(IFoodRepository foodData, IOrderRepository orderData)
    {
        _foodData = foodData;
        _orderData = orderData;
    }

    public List<SelectListItem> FoodItems { get; set; } = [];

    [BindProperty]
    public OrderModel Order { get; set; } = new();

    public async Task OnGet()
    {
        IEnumerable<FoodModel> food = await _foodData.GetFoodAsync();

        FoodItems = [];

        food.ToList().ForEach(x =>
        {
            FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
        });
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        IEnumerable<FoodModel> food = await _foodData.GetFoodAsync();
        Order.Total = Order.Quantity * food.Where(x => x.Id == Order.FoodId).First().Price;
        int id = await _orderData.CreateOrderAsync(Order);
        return RedirectToPage("./Display", new { Id = id });
    }
}