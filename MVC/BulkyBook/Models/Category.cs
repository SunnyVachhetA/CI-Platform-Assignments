using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyBook.Models;
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name cannot be empty!")]
    public string Name { get; set; }

    [Range(1, 100, ErrorMessage = "Must be between 1 to 100!")]
    [DisplayName("Display Order")] public int DisplayOrder { get; set; }

    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}