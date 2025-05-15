using System.Collections.Generic;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCDemo.Models;

public sealed class OrderCreateModel
{
    public OrderModel Order { get; set; } = new OrderModel();
    public List<SelectListItem> FoodItems { get; set; } = new List<SelectListItem>();
}
