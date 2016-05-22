using System;

namespace IdentityServer3.EntityFrameworkCore.Entities
{
    public class ClientSecret
    {
        public virtual int Id { get; set; }

        public virtual string Value { get; set; }

        public string Type { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual Client Client { get; set; }
       
    }
}