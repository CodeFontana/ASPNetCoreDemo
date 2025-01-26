using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemo.Pages.Food;

public class ListModel : PageModel
{
    private readonly IFoodData _foodData;

    public ListModel(IFoodData foodData)
    {
        _foodData = foodData;
    }

    public List<FoodModel> Food { get; set; } = [];

    public async Task OnGet()
    {
        Food = await _foodData.GetFoodAsync();
    }
}
