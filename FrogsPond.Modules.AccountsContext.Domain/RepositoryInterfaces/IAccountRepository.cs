using FrogsPond.Modules.AccountsContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrogsPond.Modules.AccountsContext.Domain
{
    public interface IAccountRepository
    {
        IList<Account> GetAll();
        Account FindById(int id);
        Account FindByToken(string token);
        Account FindByEmail(string email);
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
        void DeleteById(int id);
        void SaveChanges();
        int GetCount();

        //  x.ResetTokenExpires > DateTime.UtcNow
        Account SingleOrDefault(string token);
    }
}
