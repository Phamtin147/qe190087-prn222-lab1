using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace ProductManagementMVC.Controllers;

public sealed class ProductsController : Controller
{
    private readonly IProductService _contextProduct;
    private readonly ICategoryService _contextCategory;

    public ProductsController(IProductService contextProduct, ICategoryService contextCategory)
    {
        _contextProduct = contextProduct;
        _contextCategory = contextCategory;
    }

    public IActionResult Index()
    {
        if (!IsSignedIn()) return RedirectToAction("Login", "Account");
        ViewBag.Username = HttpContext.Session.GetString("Username");
        return View(_contextProduct.GetProducts());
    }

    public IActionResult Details(int? id)
    {
        if (!IsSignedIn()) return RedirectToAction("Login", "Account");
        if (id is null) return NotFound();
        var product = _contextProduct.GetProductById(id.Value);
        return product is null ? NotFound() : View(product);
    }

    public IActionResult Create()
    {
        if (!IsSignedIn()) return RedirectToAction("Login", "Account");
        PopulateCategories();
        return View(new Product());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (!IsSignedIn()) return RedirectToAction("Login", "Account");
        if (ModelState.IsValid)
        {
            _contextProduct.SaveProduct(product);
            return RedirectToAction(nameof(Index));
        }
        PopulateCategories(product.CategoryId);
        return View(product);
    }

    public IActionResult Edit(int? id)
    {
        if (!IsSignedIn()) return RedirectToAction("Login", "Account");
        if (id is null) return NotFound();
        var product = _contextProduct.GetProductById(id.Value);
        if (product is null) return NotFound();
        PopulateCategories(product.CategoryId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Product product)
    {
        if (!IsSignedIn()) return RedirectToAction("Login", "Account");
        if (id != product.ProductId) return NotFound();
        if (ModelState.IsValid)
        {
            _contextProduct.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }
        PopulateCategories(product.CategoryId);
        return View(product);
    }

    public IActionResult Delete(int? id)
    {
        if (!IsSignedIn()) return RedirectToAction("Login", "Account");
        if (id is null) return NotFound();
        var product = _contextProduct.GetProductById(id.Value);
        return product is null ? NotFound() : View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!IsSignedIn()) return RedirectToAction("Login", "Account");
        var product = _contextProduct.GetProductById(id);
        if (product is not null) _contextProduct.DeleteProduct(product);
        return RedirectToAction(nameof(Index));
    }

    private void PopulateCategories(int? selected = null) => ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryName", selected);

    private bool IsSignedIn() => HttpContext.Session.GetString("UserId") is not null;
}
