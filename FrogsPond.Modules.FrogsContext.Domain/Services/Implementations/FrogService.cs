
using AutoMapper;
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Entities;
using FrogsPond.Modules.FrogsContext.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<FrogResponse> Create(FrogCreateRequest model)
        {
            var frog = _mapper.Map<Frog>(model);
            frog.Created = DateTime.UtcNow;

            await _frogRepository.Add(frog);

            return _mapper.Map<FrogResponse>(frog);
        }

        public async Task Delete(string id)
        {
            await _frogRepository.Delete(id);
        }

        public async Task<IEnumerable<FrogResponse>> GetAll()
        {
            var frogs = await _frogRepository.GetAll();
            return _mapper.Map<IList<FrogResponse>>(frogs);
        }

        public async Task<IEnumerable<FrogResponse>> GetAllByUserId(string userId)
        {
            var frogs = await _frogRepository.GetAllByUserId(userId);
            return _mapper.Map<IList<FrogResponse>>(frogs);
        }

        public async Task<FrogResponse> GetById(string id)
        {
            return _mapper.Map<FrogResponse>(await GetFrogById(id));
        }

        public async Task<FrogResponse> Update(string id, FrogUpdateRequest model)
        {
            var frog = await GetFrogById(id);
            _mapper.Map(model, frog);

            frog.Updated = DateTime.UtcNow;
            await _frogRepository.Update(id, frog);

            return _mapper.Map<FrogResponse>(frog);
        }

        private async Task<Frog> GetFrogById(string id)
        {
            var frog = await _frogRepository.GetById(id);
            if (frog == null) throw new KeyNotFoundException("Frog not found");

            return frog;
        }
    }
}
