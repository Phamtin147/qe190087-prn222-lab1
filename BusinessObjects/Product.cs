using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public class Product
{
    public int ProductId { get; set; }

    [Required, StringLength(100)]
    public string ProductName { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int UnitsInStock { get; set; }

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
