using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorSSRDemo.Models;

public sealed class PersonModel
{
    [Required(ErrorMessage = "Please enter a first name.")]
    [Length(1, 50, ErrorMessage = "First name must be between 1 and 50 characters.")]
    [DisplayName("First name")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Please enter a last name.")]
    [Length(1, 50, ErrorMessage = "Last name must be between 1 and 50 characters.")]
    [DisplayName("Last name")]
    public string? LastName { get; set; }

    public static bool IsModelValid(PersonModel model, out ICollection<ValidationResult> validationResults)
    {
        ValidationContext validationContext = new(model);
        validationResults = new List<ValidationResult>();
        return Validator.TryValidateObject(model, validationContext, validationResults, true);
    }
}
