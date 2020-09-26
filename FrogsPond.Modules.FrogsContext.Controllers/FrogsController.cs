using AutoMapper;
using FrogsPond.Modules.AccountsContext.Domain.UseCases;
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrogsPond.Modules.FrogsContext.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrogsController : ControllerBase
    {

        private readonly IFrogService _frogService;
        private readonly IMapper _mapper;

        public FrogsController(IFrogService frogService, IMapper mapper)
        {
            _frogService = frogService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrogResponse>>> GetAll()
        {
            var frogs = await _frogService.GetAll();
            return Ok(frogs);
        }


        [HttpGet("{id}/{userId}")]
        public async Task<ActionResult<FrogResponse>> GetById(string id, string userId)
        {
            var frog = await _frogService.GetById(id);
            return Ok(frog);
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FrogResponse>>> GetByUserId(string userId)
        {
            var frogs = await _frogService.GetAllByUserId(userId);
            return Ok(frogs);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FrogResponse>> Create(FrogCreateRequest model)
        {
            var frog = await _frogService.Create(model);
            return Ok(frog);
        }


        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<FrogResponse>> Update(string id, FrogUpdateRequest model)
        {
            var frog = await _frogService.Update(id, model);
            return Ok(frog);
        }


        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _frogService.Delete(id);
            return Ok(new { message = "Frog deleted successfully" });
        }
    }
}
