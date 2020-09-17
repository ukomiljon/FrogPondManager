using FrogsPond.Modules.AccountsContext.Domain;
using FrogsPond.Modules.AccountsContext.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FrogsPond.Modules.AccountsContext.Data
{
    public class MongoDbRepository : IAccountRepository
    {
        public MongoDbRepository()
        {

        }

        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public void Delete(Account account)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Account FindByEmail(string email)
        {
            return new Account();
            throw new NotImplementedException();
        }

        public Account FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Account FindByToken(string token)
        {
            throw new NotImplementedException();
        }

        public IList<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Account SingleOrDefault(string token)
        {
            throw new NotImplementedException();
        }

        public void Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
