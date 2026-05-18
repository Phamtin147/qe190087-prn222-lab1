using BusinessObjects;

namespace Repositories;

public interface IAccountRepository
{
    AccountMember? GetAccountByLogin(string login);
}
