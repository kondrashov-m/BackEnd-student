using DependencyInjectionDemo.DTOs;
using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Repositories;

namespace DependencyInjectionDemo.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<ProductResponseDto> GetAllProducts()
    {
        return _repository.GetAll()
            .Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                CreatedAt = p.CreatedAt
            })
            .ToList();
    }

    public ProductResponseDto? GetProductById(int id)
    {
        var product = _repository.GetById(id);
        return product == null ? null : new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            CreatedAt = product.CreatedAt
        };
    }

    public ProductResponseDto CreateProduct(ProductCreateDto dto)
    {
        var product = new Product { Name = dto.Name };
        var created = _repository.Add(product);
        return new ProductResponseDto
        {
            Id = created.Id,
            Name = created.Name,
            CreatedAt = created.CreatedAt
        };
    }
}
