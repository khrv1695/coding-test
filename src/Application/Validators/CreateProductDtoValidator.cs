using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(p => p.Description)
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}
