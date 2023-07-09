using DataLibrary.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerDemo.Pages.Order
{
    public partial class Create
    {
        private OrderModel order = new OrderModel();
        private List<FoodModel> foodItems = new List<FoodModel>();

        protected override async Task OnParametersSetAsync()
        {
            foodItems = await foodData.GetFood();
            await base.OnParametersSetAsync();
        }

        private void FoodItemChange(ChangeEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Value.ToString()) == false)
            {
                order.FoodId = int.Parse(e.Value.ToString());
            }
            else
            {
                order.FoodId = 0;
            }
        }

        private async Task HandleValidSubmit()
        {
            order.Total = order.Quantity * foodItems.Where(x => x.Id == order.FoodId).First().Price;
            int id = await orderData.CreateOrder(order);
            navMan.NavigateTo($"order/display/{id}");
        }
    }
}
