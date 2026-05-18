using BusinessObjects;

namespace Services;

public interface IAccountService
{
    AccountMember? GetAccountByLogin(string login);
}
