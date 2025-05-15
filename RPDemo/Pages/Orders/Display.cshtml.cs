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
    private readonly IOrderRepository _orderData;
    private readonly IFoodRepository _foodData;

    public DisplayModel(IOrderRepository orderData, IFoodRepository foodData)
    {
        _orderData = orderData;
        _foodData = foodData;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public OrderUpdateModel UpdateModel { get; set; } = new();

    public OrderModel? Order { get; set; }
    public string? ItemPurchased { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Order = await _orderData.GetOrderByIdAsync(Id);

        if (Order != null)
        {
            IEnumerable<FoodModel> food = await _foodData.GetFoodAsync();
            ItemPurchased = food.Where(x => x.Id == Order.FoodId).FirstOrDefault()?.Title;
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid == false 
            || UpdateModel.Id <= 0 
            || string.IsNullOrWhiteSpace(UpdateModel.OrderName))
        {
            return Page();
        }

        await _orderData.UpdateOrderNameAsync(UpdateModel.Id, UpdateModel.OrderName);

        return RedirectToPage("./Display", new { UpdateModel.Id });
    }
}