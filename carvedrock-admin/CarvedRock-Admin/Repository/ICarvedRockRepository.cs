using CarvedRock_Admin.Data;

namespace CarvedRock_Admin.Repository;
public interface ICarvedRockRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product> AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task RemoveProductAsync(int productIdToRemove);
}