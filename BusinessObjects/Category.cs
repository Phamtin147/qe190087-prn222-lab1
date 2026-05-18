using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public class Category
{
    public int CategoryId { get; set; }

    [Required, StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = [];
}
