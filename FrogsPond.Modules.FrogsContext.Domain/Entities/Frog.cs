﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FrogsPond.Modules.FrogsContext.Domain.Entities
{
    public class Frog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Alife { get; set; }
        public int Age { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
