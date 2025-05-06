using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerDemo.Models;

public sealed class OrderUpdateModel
{
    public int Id { get; set; }

    [DisplayName("Name of the Order")]
    [MaxLength(20, ErrorMessage = "You need to keep the name to a max of 20 characters")]
    [MinLength(3, ErrorMessage = "You need to enter at least 3 characters for an order name")]
    [Required]
    public string? OrderName { get; set; }
}