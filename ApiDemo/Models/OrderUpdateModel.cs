using System.ComponentModel.DataAnnotations;

namespace ApiDemo.Models;

public sealed class OrderUpdateModel
{
    [Required(ErrorMessage = "Order ID is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "New order name is required")]
    public string? OrderName { get; set; }
}