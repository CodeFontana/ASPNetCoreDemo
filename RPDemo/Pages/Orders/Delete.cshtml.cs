using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemo.Pages.Orders;

public class DeleteModel : PageModel
{
    private readonly IFoodOrderRepository _orderData;

    public DeleteModel(IFoodOrderRepository orderData)
    {
        _orderData = orderData;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public FoodOrderModel? Order { get; set; }

    public async Task OnGet()
    {
        Order = await _orderData.GetOrderByIdAsync(Id);
    }

    public async Task<IActionResult> OnPost()
    {
        await _orderData.DeleteOrderAsync(Id);
        return RedirectToPage("./Create");
    }
}
