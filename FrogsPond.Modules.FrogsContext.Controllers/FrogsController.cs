using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrogsPond.Modules.FrogsContext.Controllers
{
    [ApiController]
    [Route("api/frogs")]
    public class FrogsController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {

            return "hello from frogs " + new Random().Next(100);
        }
    }
}
