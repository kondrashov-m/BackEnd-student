using DependencyInjectionDemo.DTOs;

namespace DependencyInjectionDemo.Services;

public interface IProductService
{
    List<ProductResponseDto> GetAllProducts();
    ProductResponseDto? GetProductById(int id);
    ProductResponseDto CreateProduct(ProductCreateDto dto);
}
