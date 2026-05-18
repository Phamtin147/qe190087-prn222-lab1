using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ProductManagementMVC.Controllers;

public sealed class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Login() => View(new AccountMember());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(AccountMember model)
    {
        var account = _accountService.GetAccountByLogin(model.EmailAddress);
        if (account is not null && account.MemberPassword == model.MemberPassword)
        {
            HttpContext.Session.SetString("UserId", account.MemberId.ToString());
            HttpContext.Session.SetString("Username", account.FullName);
            return RedirectToAction("Index", "Products");
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
