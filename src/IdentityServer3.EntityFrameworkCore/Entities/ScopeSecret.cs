﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer3.EntityFrameworkCore.Entities
{
    public class ScopeSecret
    {
        public virtual int Id { get; set; }

        public string Description { get; set; }

        public DateTime? Expiration { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public virtual Scope Scope { get; set; }
    }
}