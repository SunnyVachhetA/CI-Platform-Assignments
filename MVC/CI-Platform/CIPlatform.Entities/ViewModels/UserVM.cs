using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels;
public class UserVM
{
    public long? UserId { get; set; }
    [Required(ErrorMessage = "Please enter first name!")]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter last name!")]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter valid email ID!")]
    [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Invalid Email ID!")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password cannot be empty!")]
    
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Please enter valid phone number")]
    [StringLength(10, ErrorMessage = "Phone number should be of length 10!")]
    [DisplayName("Phone Number")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "Phone number should contains only numbers!")]
    public string PhoneNumber { get; set; } = null!;

    public override string ToString()
    {
        return UserId + " " + FirstName;
    }
}
