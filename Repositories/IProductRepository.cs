using DependencyInjectionDemo.Models;

namespace DependencyInjectionDemo.Repositories;

public interface IProductRepository
{
    List<Product> GetAll();
    Product? GetById(int id);
    Product Add(Product product);
}
