using FrogsPond.Modules.FrogsContext.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrogsPond.Modules.FrogsContext.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public Frog Frog => (Frog)HttpContext.Items["Frog"];
    }
}
