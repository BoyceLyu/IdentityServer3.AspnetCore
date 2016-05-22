using System;

namespace IdentityServer3.EntityFrameworkCore.Entities
{
    public class ClientCustomGrantType
    {
        public virtual int Id { get; set; }

        public virtual string GrantType { get; set; }

        public virtual Client Client { get; set; }
    }
}