using AutoMapper;
using FrogsPond.Modules.AccountsContext.Domain.UseCases;
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Services;
using FrogsPond.Modules.AccountsContext.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FrogsPond.Modules.FrogsContext.Domain.Entities;
using System.Linq;

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
            var account = (Account)Request.HttpContext.Items["Account"];
            if (account == null || account.Role == Role.Admin)
            {
                var frogs = await _frogService.GetAll();
                return Ok(frogs);
            }

            return Ok(null);
        }


        [HttpGet("{id}/{userId}")]
        public async Task<ActionResult<FrogResponse>> GetById(string id, string userId)
        {
            var frog = await _frogService.GetById(id);
            return Ok(frog);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FrogResponse>>> GetByUserId(string userId)
        {
            var account = (Account)Request.HttpContext.Items["Account"];
            var frogs = await _frogService.GetAllByUserId(userId);

            if (account.Role == Role.Admin) return Ok(frogs);
            frogs = frogs.Where(item => item.UserId == account.Id);

            return Ok(frogs);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FrogResponse>> Create(FrogCreateRequest model)
        {
            var account = (Account)Request.HttpContext.Items["Account"];
            var frogModel = _mapper.Map<Frog>(model);
            frogModel.UserId = account.Id;
            var frog = await _frogService.Create(frogModel);
            return Ok(frog);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<FrogResponse>> Update(string id, FrogUpdateRequest model)
        {
            var account = (Account)Request.HttpContext.Items["Account"];
            var foundFrog = await _frogService.GetById(id);
            if (foundFrog.UserId == account.Id || account.Role == Role.Admin)
            {
                var frog = await _frogService.Update(id, model);
                return Ok(frog);
            }

            return Ok(new { message = "you are not authorized to update this frog" });
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var account = (Account)Request.HttpContext.Items["Account"];
            var foundFrog = await _frogService.GetById(id);
            if (foundFrog.UserId == account.Id || account.Role == Role.Admin)
            {
                await _frogService.Delete(id);
                return Ok(new { message = "Frog deleted successfully" });
            }

            return Ok(new { message = "you are not authorized to delete this frog" });
        }
    }
}
