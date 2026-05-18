using BusinessObjects;
using Repositories;

namespace Services;

public sealed class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public AccountMember? GetAccountByLogin(string login) => _accountRepository.GetAccountByLogin(login);
}
