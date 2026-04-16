using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemo.Pages.Orders;

public class ListModel : PageModel
{
    private readonly IFoodOrderRepository _orderData;

    public ListModel(IFoodOrderRepository orderData)
    {
        _orderData = orderData;
    }

    public IEnumerable<FoodOrderSummaryModel> Orders { get; set; } = [];

    public async Task OnGet()
    {
        Orders = await _orderData.GetAllOrdersAsync();
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        await _orderData.DeleteOrderAsync(id);
        return RedirectToPage();
    }
}
