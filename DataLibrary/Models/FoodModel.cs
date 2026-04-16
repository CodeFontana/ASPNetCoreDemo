using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public sealed class FoodModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Title must be 50 characters or fewer")]
    [DisplayName("Item")]
    public string? Title { get; set; }

    [Required]
    [MaxLength(250, ErrorMessage = "Description must be 250 characters or fewer")]
    public string? Description { get; set; }

    [Required]
    [Range(0.01, 999.99, ErrorMessage = "Price must be between $0.01 and $999.99")]
    public decimal Price { get; set; }
}
