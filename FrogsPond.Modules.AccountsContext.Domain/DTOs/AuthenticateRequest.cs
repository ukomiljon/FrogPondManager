
using System.ComponentModel.DataAnnotations;

namespace FrogsPond.Modules.AccountsContext.Domain.UseCases.DTO
{
    public class AuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
