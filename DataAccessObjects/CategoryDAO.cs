using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public sealed class CategoryDAO
{
    private readonly MyStoreContext _context;

    public CategoryDAO(MyStoreContext context)
    {
        _context = context;
    }

    public List<Category> GetCategories() => _context.Categories.AsNoTracking().OrderBy(c => c.CategoryName).ToList();
}
