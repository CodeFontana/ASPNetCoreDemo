using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemo.Pages.Food;

public class EditModel : PageModel
{
    private readonly IFoodRepository _foodData;

    public EditModel(IFoodRepository foodData)
    {
        _foodData = foodData;
    }

    [BindProperty]
    public FoodModel? Food { get; set; }

    public async Task<IActionResult> OnGet(int id)
    {
        IEnumerable<FoodModel> allFood = await _foodData.GetFoodAsync();
        Food = allFood.FirstOrDefault(f => f.Id == id);

        if (Food is null)
        {
            return Page();
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid || Food is null)
        {
            return Page();
        }

        await _foodData.UpdateFoodAsync(Food);
        return RedirectToPage("./List");
    }
}
