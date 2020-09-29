using System.ComponentModel.DataAnnotations;

namespace FrogsPond.Modules.FrogsContext.Domain.DTOs
{
    public class FrogCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Color { get; set; }
       
    }
}
