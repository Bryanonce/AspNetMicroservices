using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repository
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProductById(string id);
        public Task<IEnumerable<Product>> GetProductByName(string name);
        public Task<IEnumerable<Product>> GetProductByCategory(string category);
        public Task CreateProduct(Product product);
        public Task<bool> UpdateProduct(Product product);
        public Task<bool> DeleteProduct(string id);
    }
}
