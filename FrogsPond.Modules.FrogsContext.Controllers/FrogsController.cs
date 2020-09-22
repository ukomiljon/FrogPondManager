using System;
using System.Collections.Generic;
using AutoMapper;
using FrogsPond.Modules.AccountsContext.Domain.Entities;
using FrogsPond.Modules.AccountsContext.Domain.UseCases;
using FrogsPond.Modules.FrogsContext.Domain.DTOs;
using FrogsPond.Modules.FrogsContext.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [Authorize(Role.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<FrogResponse>> GetAll()
        {
            var frogs = _frogService.GetAll();
            return Ok(frogs);
        }

        [Authorize(Role.Admin)]
        [AuthorizeUserAttribute]
        [HttpGet("{id}/{userId}")]
        public ActionResult<FrogResponse> GetById(string id, string userId)
        {
            var frog = _frogService.GetById(id);
            return Ok(frog);
        }

        [Authorize(Role.Admin)]
        [AuthorizeUserAttribute]
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<FrogResponse>> GetByUserId(string userId)
        {
            var frogs = _frogService.GetAllByUserId(userId);
            return Ok(frogs);
        }

        [Authorize(Role.Admin)]
        [AuthorizeUserAttribute]
        [HttpPost]
        public ActionResult<FrogResponse> Create(FrogCreateRequest model)
        {
            var frog = _frogService.Create(model);
            return Ok(frog);
        }

        [Authorize(Role.Admin)]
        [AuthorizeUserAttribute]
        [HttpPut("{id:int}")]
        public ActionResult<FrogResponse> Update(string id, FrogUpdateRequest model)
        {
            var frog = _frogService.Update(id, model);
            return Ok(frog);
        }

        [Authorize(Role.Admin)]
        [AuthorizeUserAttribute]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(string id)
        {
            _frogService.Delete(id);
            return Ok(new { message = "Frog deleted successfully" });
        }
    }
}
