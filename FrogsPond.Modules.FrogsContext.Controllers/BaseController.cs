using FrogsPond.Modules.FrogsContext.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrogsPond.Modules.FrogsContext.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public Frog Account => (Frog)HttpContext.Items["Frog"];
    }
}
