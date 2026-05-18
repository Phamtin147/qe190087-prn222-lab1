using BusinessObjects;
using DataAccessObjects;

namespace Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly CategoryDAO _categoryDao;

    public CategoryRepository(CategoryDAO categoryDao)
    {
        _categoryDao = categoryDao;
    }

    public List<Category> GetCategories() => _categoryDao.GetCategories();
}
