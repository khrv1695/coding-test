using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductDto> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Getting product with id: {id}", id);
        var product = await _unitOfWork.ProductRepository.GetProductAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        _logger.LogInformation("Getting all products");
        var products = await _unitOfWork.ProductRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        _logger.LogInformation("Creating product");
        var product = _mapper.Map<Product>(createProductDto);
        var createdProduct = await _unitOfWork.ProductRepository.CreateProductAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        _logger.LogInformation("Updating product with id: {id}", updateProductDto.Id);
        var product = _mapper.Map<Product>(updateProductDto);
        await _unitOfWork.ProductRepository.UpdateProductAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        _logger.LogInformation("Deleting product with id: {id}", id);
        await _unitOfWork.ProductRepository.DeleteProductAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
