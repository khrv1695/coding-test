using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UpdateProductDto
{
    [Required]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
