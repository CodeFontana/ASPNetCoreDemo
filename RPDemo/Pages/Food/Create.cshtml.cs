using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemo.Pages.Food;

public class CreateModel : PageModel
{
    private readonly IFoodRepository _foodData;

    public CreateModel(IFoodRepository foodData)
    {
        _foodData = foodData;
    }

    [BindProperty]
    public FoodModel Food { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _foodData.CreateFoodAsync(Food);
        return RedirectToPage("./List");
    }
}
