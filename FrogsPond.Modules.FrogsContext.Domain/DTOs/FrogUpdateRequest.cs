﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrogsPond.Modules.FrogsContext.Domain.DTOs
{
    public class FrogUpdateRequest
    {
        public string Name { get; set; }
        public bool Alife { get; set; }
        public int Age { get; set; }
        public string UserId { get; set; }
    }
}
