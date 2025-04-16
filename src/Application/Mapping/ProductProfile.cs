using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}
