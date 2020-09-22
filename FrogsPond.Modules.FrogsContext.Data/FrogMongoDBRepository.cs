using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Entities;
using FrogsPond.Modules.FrogsContext.Domain.RepositoryInterfaces;
using FrogsPond.Modules.FrogsContext.Domain.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrogsPond.Modules.FrogsContext.Data
{
    public class FrogMongoDBRepository : IFrogRepository
    {
        private readonly IMongoCollection<Frog> _frogs;
        public FrogMongoDBRepository(IFrogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _frogs = database.GetCollection<Frog>(settings.FrogCollectionName);
        }
        public async Task<Frog> Add(Frog frog)
        {
            await _frogs.InsertOneAsync(frog);
            return await _frogs.Find<Frog>(doc => doc.Name == frog.Name).FirstOrDefaultAsync();
        }

        public async Task Delete(string id)
        {
            await _frogs.DeleteOneAsync(doc => doc.Name == id);
        }

        public async Task<IEnumerable<Frog>> GetAll()
        {
            return await _frogs.Find(doc => true).ToListAsync();
        }

        public async Task<IEnumerable<Frog>> GetAllByUserId(string userId)
        {
            return await _frogs.Find(doc => doc.UserId == userId).ToListAsync();
        }

        public async Task<Frog> GetById(string id)
        {
            return await _frogs.Find(doc => doc.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(string id, Frog frog)
        {
            await _frogs.ReplaceOneAsync(doc => doc.Id == id, frog);
        }
    }
}
