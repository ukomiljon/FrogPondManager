using System.ComponentModel.DataAnnotations;

namespace FrogsPond.Modules.AccountsContext.Domain.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
