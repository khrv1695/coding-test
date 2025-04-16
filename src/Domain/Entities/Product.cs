using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
