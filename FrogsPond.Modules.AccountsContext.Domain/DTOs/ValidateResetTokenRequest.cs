using System.ComponentModel.DataAnnotations;

namespace FrogsPond.Modules.AccountsContext.Domain.Models
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
