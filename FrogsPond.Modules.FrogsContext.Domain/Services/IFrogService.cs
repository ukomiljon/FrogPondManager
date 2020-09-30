
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrogsPond.Modules.FrogsContext.Domain.Services
{
    public interface IFrogService
    {
        Task<IEnumerable<FrogResponse>> GetAll();
        Task<IEnumerable<FrogResponse>> GetAllByUserId(string userId);
        Task<FrogResponse> GetById(string id);
        Task<FrogResponse> Create(Frog model);
        Task<FrogResponse> Update(string id, FrogUpdateRequest model);
        Task Delete(string id);
    }
}
