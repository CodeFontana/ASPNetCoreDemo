using System.Collections.Generic;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCDemo.Models;

public sealed class FoodOrderCreateModel
{
    public FoodOrderModel Order { get; set; } = new();
    public List<SelectListItem> FoodItems { get; set; } = [];
}
