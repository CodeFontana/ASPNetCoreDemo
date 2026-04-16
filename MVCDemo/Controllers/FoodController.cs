using System;
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

    public IActionResult Create()
    {
        return View(new FoodModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(FoodModel food)
    {
        if (ModelState.IsValid == false)
        {
            return View(food);
        }

        await _foodData.CreateFoodAsync(food);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        IEnumerable<FoodModel> allFood = await _foodData.GetFoodAsync();
        FoodModel? food = null;

        foreach (FoodModel f in allFood)
        {
            if (f.Id == id)
            {
                food = f;
                break;
            }
        }

        if (food is null)
        {
            return NotFound();
        }

        return View(food);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(FoodModel food)
    {
        if (ModelState.IsValid == false)
        {
            return View(food);
        }

        await _foodData.UpdateFoodAsync(food);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _foodData.DeleteFoodAsync(id);
        return RedirectToAction("Index");
    }
}
