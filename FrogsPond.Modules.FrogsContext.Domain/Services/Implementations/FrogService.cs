
using AutoMapper;
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Entities;
using FrogsPond.Modules.FrogsContext.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrogsPond.Modules.FrogsContext.Domain.Services.Implementations
{
    public class FrogService : IFrogService
    {
        private readonly IFrogRepository _frogRepository;
        private readonly IMapper _mapper;

        public FrogService(IFrogRepository frogRepository, IMapper mapper)
        {
            _frogRepository = frogRepository;
            _mapper = mapper;
        }

        public FrogResponse Create(FrogCreateRequest model)
        {
            var frog = _mapper.Map<Frog>(model);
            frog.Created = DateTime.UtcNow;

            _frogRepository.Add(frog);

            return _mapper.Map<FrogResponse>(frog);
        }

        public void Delete(string id)
        {
            _frogRepository.Delete(id);
        }

        public IEnumerable<FrogResponse> GetAll()
        {
            var frogs = _frogRepository.GetAll();
            return _mapper.Map<IList<FrogResponse>>(frogs);
        }

        public IEnumerable<FrogResponse> GetAllByUserId(string userId)
        {
            var frogs = _frogRepository.GetAllByUserId(userId);
            return _mapper.Map<IList<FrogResponse>>(frogs);
        }

        public FrogResponse GetById(string id)
        {            
            return _mapper.Map<FrogResponse>(GetFrogById(id));
        }

        public FrogResponse Update(string id, FrogUpdateRequest model)
        {
            var frog = GetFrogById(id);
            _mapper.Map(model, frog);

            frog.Updated = DateTime.UtcNow;
            _frogRepository.Update(id, frog);

            return _mapper.Map<FrogResponse>(frog);
        }

        private Frog GetFrogById(string id)
        {
            var frog = _frogRepository.GetById(id).Result;
            if (frog == null) throw new KeyNotFoundException("Frog not found");

            return frog;
        }
    }
}
