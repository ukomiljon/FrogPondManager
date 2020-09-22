using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
