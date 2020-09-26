using FrogsPond.Modules.AccountsContext.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrogsPond.Modules.AccountsContext.Domain
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAll();
        Task<Account> FindById(string id);
        Task<Account> FindByVerificationToken(string token);

        Task<Account> FindValidatedResetToken(string token);
        Task<Account> FindOneFromRefreshTokens(string token);
        Task<Account> FindByEmail(string email);
        Task<Account> Add(Account account);
        Task Update(Account account);
        Task Delete(Account account);
        Task DeleteById(string id);
        Task<int> GetCount();
    }
}
