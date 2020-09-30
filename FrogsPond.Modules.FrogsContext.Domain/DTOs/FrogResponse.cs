using System;

namespace FrogsPond.Modules.FrogsContext.Domain.DTOs
{
    public class FrogResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
