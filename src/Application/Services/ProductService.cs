using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> GetProductAsync(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetProductAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);
        var createdProduct = await _unitOfWork.ProductRepository.CreateProductAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        var product = _mapper.Map<Product>(updateProductDto);
        await _unitOfWork.ProductRepository.UpdateProductAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _unitOfWork.ProductRepository.DeleteProductAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
