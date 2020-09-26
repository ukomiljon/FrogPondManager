using System.ComponentModel.DataAnnotations;

namespace FrogsPond.Modules.FrogsContext.Domain.DTOs
{
    public class FrogCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Alife { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
