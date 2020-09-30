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
            var frogs = await _frogService.GetAll();
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
