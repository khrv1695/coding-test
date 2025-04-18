using MediatR;
using Application.DTOs;

namespace Application.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public CreateProductDto CreateProductDto { get; set; }

        public CreateProductCommand(CreateProductDto createProductDto)
        {
            CreateProductDto = createProductDto;
        }
    }
}
