using BusinessObjects;
using DataAccessObjects;

namespace Repositories;

public sealed class AccountRepository : IAccountRepository
{
    private readonly AccountDAO _accountDao;

    public AccountRepository(AccountDAO accountDao)
    {
        _accountDao = accountDao;
    }

    public AccountMember? GetAccountByLogin(string login) => _accountDao.GetAccountByLogin(login);
}
