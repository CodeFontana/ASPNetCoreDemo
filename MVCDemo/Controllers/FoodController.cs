using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemo.Controllers;

public class FoodController : Controller
{
    private readonly IFoodData _foodData;

    public FoodController(IFoodData foodData)
    {
        _foodData = foodData;
    }

    public async Task<IActionResult> Index()
    {
        List<FoodModel> food = await _foodData.GetFoodAsync();
        return View(food);
    }
}
