using System;
using System.ComponentModel.DataAnnotations;

namespace ThreeTier.Entities.ViewModels;
public class EmployeeModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required field.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    public string? Role { get; set; } = string.Empty;
}
