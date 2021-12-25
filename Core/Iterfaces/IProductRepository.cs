using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Iterfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        
    }
}