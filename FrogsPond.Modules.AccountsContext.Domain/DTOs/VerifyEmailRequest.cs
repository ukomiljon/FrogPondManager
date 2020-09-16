using System.ComponentModel.DataAnnotations;

namespace FrogsPond.Modules.AccountsContext.Domain.Models
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
