using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrogsPond.Modules.AccountsContext.Domain
{
    public class SmtpSettings : ISmtpSettings
    {
        public string Secret { get; set; }
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }

    }

    public interface ISmtpSettings
    {
        string SmtpPass { get; set; }
        string Secret { get; set; }
        string EmailFrom { get; set; }
        string SmtpHost { get; set; }
        int SmtpPort { get; set; }
        string SmtpUser { get; set; }
    }
}
