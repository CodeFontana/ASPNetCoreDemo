using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodData _foodData;

        public FoodController(IFoodData foodData)
        {
            _foodData = foodData;
        }

        [HttpGet]
        public async Task<List<FoodModel>> Get()
        {
            return await _foodData.GetFood();
        }
    }
}
