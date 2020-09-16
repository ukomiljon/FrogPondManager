using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrogsPond.Modules.AccountsContext.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {

            return "hello from accounts " + new Random().Next(100); ;
        }
    }
}
