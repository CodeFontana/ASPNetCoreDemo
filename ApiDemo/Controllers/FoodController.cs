using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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
    [EnableRateLimiting("fixed")]
    public async Task<IEnumerable<FoodModel>> GetFood()
    {
        return await _foodData.GetFoodAsync();
    }
}
