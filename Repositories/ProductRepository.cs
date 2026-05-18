using BusinessObjects;
using DataAccessObjects;

namespace Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly ProductDAO _productDao;

    public ProductRepository(ProductDAO productDao)
    {
        _productDao = productDao;
    }

    public List<Product> GetProducts() => _productDao.GetProducts();

    public Product? GetProductById(int id) => _productDao.GetProductById(id);

    public void SaveProduct(Product product) => _productDao.SaveProduct(product);

    public void UpdateProduct(Product product) => _productDao.UpdateProduct(product);

    public void DeleteProduct(Product product) => _productDao.DeleteProduct(product);
}
