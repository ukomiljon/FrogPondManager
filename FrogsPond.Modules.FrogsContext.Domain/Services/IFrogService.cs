 
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrogsPond.Modules.FrogsContext.Domain.Services
{
   public interface IFrogService
    {
        IEnumerable<FrogResponse> GetAll();
        IEnumerable<FrogResponse> GetAllByUserId(string userId);
        FrogResponse GetById(string id);
        FrogResponse Create(FrogCreateRequest model);
        FrogResponse Update(string id, FrogUpdateRequest model);
        void Delete(string id);
    }
}
