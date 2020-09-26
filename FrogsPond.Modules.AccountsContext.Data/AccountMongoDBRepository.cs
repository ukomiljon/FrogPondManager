using FrogsPond.Modules.AccountsContext.Domain;
using FrogsPond.Modules.AccountsContext.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrogsPond.Modules.AccountsContext.Data
{
    public class AccountMongoDBRepository : IAccountRepository
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountMongoDBRepository(IAccountDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _accounts = database.GetCollection<Account>(settings.AccountCollectionName);
        }

        public async Task<Account> Add(Account account)
        {
            await _accounts.InsertOneAsync(account);
            return await FindByEmail(account.Email);
        }

        public async Task Delete(Account account)
        {
            await _accounts.DeleteOneAsync(s => s.Id == account.Id);
        }

        public async Task DeleteById(string id)
        {
            await _accounts.DeleteOneAsync(s => s.Id == id);
        }

        public async Task<Account> FindByEmail(string email)
        {
            return await _accounts.Find<Account>(s => s.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Account> FindById(string id)
        {
            return await _accounts.Find<Account>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Account> FindByVerificationToken(string token)
        {
            return await _accounts.Find<Account>(x =>
                x.ResetToken == token &&
                x.ResetTokenExpires > DateTime.UtcNow).FirstOrDefaultAsync();
        }

        public async Task<Account> FindValidatedResetToken(string token)
        {
            return await _accounts.Find<Account>(s => s.VerificationToken == token).FirstOrDefaultAsync();
        }

        public async Task<Account> FindOneFromRefreshTokens(string token)
        {
            return await _accounts.Find<Account>(u => u.RefreshTokens.Any(t => t.Token == token)).FirstOrDefaultAsync();
        }

        public async Task<List<Account>> GetAll()
        {
            return await _accounts.Find(s => true).ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return (int)await _accounts.EstimatedDocumentCountAsync();
        }

        public async Task Update(Account account)
        {
            await _accounts.ReplaceOneAsync(s => s.Id == account.Id, account);
        }
    }
}
