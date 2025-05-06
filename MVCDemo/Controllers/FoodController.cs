using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemo.Controllers;

public class FoodController : Controller
{
    private readonly IFoodRepository _foodData;

    public FoodController(IFoodRepository foodData)
    {
        _foodData = foodData;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<FoodModel> food = await _foodData.GetFoodAsync();
        return View(food);
    }
}
