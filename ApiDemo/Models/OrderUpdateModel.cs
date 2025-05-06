using System.ComponentModel.DataAnnotations;

namespace ApiDemoApp.Models;

public class OrderUpdateModel
{
    [Required(ErrorMessage = "Order ID is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "New order name is required")]
    public string? OrderName { get; set; }
}