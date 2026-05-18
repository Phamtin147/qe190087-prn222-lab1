using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public sealed class ProductDAO
{
    private readonly MyStoreContext _context;

    public ProductDAO(MyStoreContext context)
    {
        _context = context;
    }

    public List<Product> GetProducts() => _context.Products.AsNoTracking().Include(p => p.Category).OrderBy(p => p.ProductId).ToList();

    public Product? GetProductById(int id) => _context.Products.AsNoTracking().Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);

    public void SaveProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}
