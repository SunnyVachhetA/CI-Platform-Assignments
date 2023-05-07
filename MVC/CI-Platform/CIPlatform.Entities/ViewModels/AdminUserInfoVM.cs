using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Entities.ViewModels;
public class AdminUserInfoVM
{
    public long UserId { get; set; }

    [Display(Name="User Avatar")]
    public string Avatar { get; set; } = string.Empty;

    [Display(Name="User Avatar")]
    public IFormFile? UserAvatar { get; set; } = null!;

    [Required]
    [Display(Name = "Name*")]
    [MinLength(3, ErrorMessage = "First name should have at least 3 characters.")]
    [MaxLength(16, ErrorMessage = "First name can have max 16 characters.")]
    [RegularExpression(@"^\S+$", ErrorMessage = "First name must not contain only whitespace characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Surname*")]
    [MinLength(3, ErrorMessage = "Surname should have at least 3 characters.")]
    [MaxLength(16, ErrorMessage = "Surname can have max 16 characters.")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Last name must not contain only whitespace characters.")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Employee ID")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Employee ID must not contain only whitespace characters.")]
    public string? EmployeeId { get; set; }

    [Display(Name="User Email*")]
    [Required]
    [EmailAddress(ErrorMessage = "Please enter valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Display(Name="Job Title")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Job title must not contain only whitespace characters.")]
    public string? Title { get; set; }

    [Display(Name = "Phone Number*")]
    [Required]
    [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number should be of 10 digit.")]
    public string? PhoneNumber { get; set; }


    public string? Department { get; set; }

    [Required]
    [MinLength(15, ErrorMessage = "At least 15 characters required.")]
    [Display(Name = "User Profile Text*")]
    [RegularExpression(@"^\S+$", ErrorMessage = "Profile Text must not contain only whitespace characters.")]

    public string Profile { get; set; } = string.Empty;
    

    [Required(ErrorMessage = "Please select country.")]
    [Display(Name = "Country*")]
    public byte CountryId { get; set; }

    [Required(ErrorMessage = "Please select city.")]
    [Display(Name = "City*")]
    public int CityId { get; set; }

    [Required]
    [Display(Name = "New Password")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
        ErrorMessage = "Password have at least 8 character, 1 lower, 1 upper and special symbol.")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Confirm Password must be same as New Password.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public IEnumerable<CityVM> CityList { get; set; } = new List<CityVM>();
    public IEnumerable<CountryVM> CountryList { get; set; } = new List<CountryVM>();

    [Required]
    public bool Status { get; set; } = false;

}
