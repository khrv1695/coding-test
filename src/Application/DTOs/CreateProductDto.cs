using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
