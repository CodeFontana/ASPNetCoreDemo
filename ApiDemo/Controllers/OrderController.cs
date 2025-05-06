using System.Linq;
using System.Threading.Tasks;
using ApiDemoApp.Models;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IFoodRepository _foodData;
    private readonly IOrderRepository _orderData;

    public OrderController(IFoodRepository foodData, IOrderRepository orderData)
    {
        _foodData = foodData;
        _orderData = orderData;
    }

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(OrderModel order)
    {
        var food = await _foodData.GetFoodAsync();
        order.Total = order.Quantity * food.Where(x => x.Id == order.FoodId).First().Price;
        int id = await _orderData.CreateOrderAsync(order);
        return Ok(new { Id = id });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var order = await _orderData.GetOrderByIdAsync(id);

        if (order != null)
        {
            var food = await _foodData.GetFoodAsync();

            var output = new
            {
                Order = order,
                ItemPurchased = food.Where(x => x.Id == order.FoodId).FirstOrDefault()?.Title
            };

            return Ok(output);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody] OrderUpdateModel data)
    {
        if (data.Id <= 0)
        {
            return BadRequest("Invalid order ID.");
        }
        else if (string.IsNullOrWhiteSpace(data.OrderName))
        {
            return BadRequest("Order name cannot be empty.");
        }

        await _orderData.UpdateOrderNameAsync(data.Id, data.OrderName);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderData.DeleteOrderAsync(id);
        return Ok();
    }
}