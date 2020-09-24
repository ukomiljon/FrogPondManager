using System;
using System.Collections.Generic;
using System.Text;

namespace FrogsPond.Modules.AccountsContext.Domain.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
        string GetSecret();
    }
}
