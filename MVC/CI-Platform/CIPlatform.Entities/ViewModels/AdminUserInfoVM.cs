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

    [Required(ErrorMessage = "Name is required!")]
    [Display(Name = "Name*")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required!")]
    [Display(Name = "Surname*")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Employee ID")]
    public string? EmployeeId { get; set; }

    [Display(Name="User Email*")]
    [Required(ErrorMessage = "User email is required!")]
    [EmailAddress(ErrorMessage = "Please enter valid email address!")]
    public string Email { get; set; } = string.Empty;

    [Display(Name="Job Title")]
    public string? Title { get; set; }

    [Display(Name = "Phone Number*")]
    [Required(ErrorMessage = "Please enter phone number!")]
    [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number should contains only numbers!")]
    public string? PhoneNumber { get; set; }


    public string? Department { get; set; }

    [Required(ErrorMessage = "User profile cannot be empty!")]
    [MinLength(15, ErrorMessage = "At least 15 characters required!")]
    [Display(Name = "User Profile Text*")]
    public string Profile { get; set; } = string.Empty;
    

    [Required(ErrorMessage = "Please select country!")]
    [Display(Name = "Country*")]
    public byte CountryId { get; set; }

    [Required(ErrorMessage = "Please select city!")]
    [Display(Name = "City*")]
    public int CityId { get; set; }

    [Required(ErrorMessage = "Enter New Password")]
    [Display(Name = "New Password")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
        ErrorMessage = "Password have at least 8 character, 1 lower, 1 upper and special symbol!")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter Confirm Password")]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Confirm Password must be same as New Password!")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public IEnumerable<CityVM> CityList { get; set; } = new List<CityVM>();
    public IEnumerable<CountryVM> CountryList { get; set; } = new List<CountryVM>();

    [Required]
    public bool Status { get; set; } = false;

}
