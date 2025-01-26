using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPDemo.Models;

namespace RPDemo.Pages.Orders;

public class DisplayModel : PageModel
{
    private readonly IOrderData _orderData;
    private readonly IFoodData _foodData;

    public DisplayModel(IOrderData orderData, IFoodData foodData)
    {
        _orderData = orderData;
        _foodData = foodData;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public OrderUpdateModel UpdateModel { get; set; } = new();

    public OrderModel Order { get; set; } = new();
    public string? ItemPurchased { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Order = await _orderData.GetOrderById(Id);

        if (Order != null)
        {
            List<FoodModel> food = await _foodData.GetFoodAsync();
            ItemPurchased = food.Where(x => x.Id == Order.FoodId).FirstOrDefault()?.Title;
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        await _orderData.UpdateOrderName(UpdateModel.Id, UpdateModel.OrderName);

        return RedirectToPage("./Display", new { UpdateModel.Id });
    }
}