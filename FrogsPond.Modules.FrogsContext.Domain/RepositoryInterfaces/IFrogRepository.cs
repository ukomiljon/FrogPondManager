 
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrogsPond.Modules.FrogsContext.Domain.RepositoryInterfaces
{
   public interface IFrogRepository
    {
        Task<IEnumerable<Frog>> GetAll();
        Task<IEnumerable<Frog>> GetAllByUserId(string userId);
        Task<Frog> GetById(string id);
        Task<Frog> Add(Frog frog);
        Task Update(string id, Frog frog);
        Task Delete(string id);
    }
}
