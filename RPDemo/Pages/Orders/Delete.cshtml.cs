using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemo.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderData _orderData;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public OrderModel Order { get; set; }

        public DeleteModel(IOrderData orderData)
        {
            _orderData = orderData;
        }

        public async Task OnGet()
        {
            Order = await _orderData.GetOrderById(Id);
        }

        public async Task<IActionResult> OnPost()
        {
            await _orderData.DeleteOrder(Id);
            return RedirectToPage("./Create");
        }
    }
}
