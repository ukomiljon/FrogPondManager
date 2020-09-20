using FrogsPond.Modules.AccountsContext.Domain;
using FrogsPond.Modules.AccountsContext.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FrogsPond.Modules.AccountsContext.Data
{
    public class AccountMongoDBRepository : IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;
        private readonly IMongoCollection<RefreshToken> _refreshTokens;

        public AccountMongoDBRepository(IAccountDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _accounts = database.GetCollection<Account>(settings.AccountCollectionName);
            _refreshTokens = database.GetCollection<RefreshToken>(settings.RefreshTokenCollectionName);
            
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
            return _accounts.Find(emp => true).ToList();
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
