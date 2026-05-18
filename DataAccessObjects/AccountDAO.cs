using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

public sealed class AccountDAO
{
    private readonly MyStoreContext _context;

    public AccountDAO(MyStoreContext context)
    {
        _context = context;
    }

    public AccountMember? GetAccountByLogin(string login) =>
        _context.AccountMembers.AsNoTracking().SingleOrDefault(a => (a.UserName != null && a.UserName == login) || a.EmailAddress == login);
}
