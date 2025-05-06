using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodController : ControllerBase
{
    private readonly IFoodRepository _foodData;

    public FoodController(IFoodRepository foodData)
    {
        _foodData = foodData;
    }

    [HttpGet]
    public async Task<IEnumerable<FoodModel>> Get()
    {
        return await _foodData.GetFoodAsync();
    }
}
