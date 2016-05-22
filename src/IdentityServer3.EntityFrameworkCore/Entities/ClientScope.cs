using System;

namespace IdentityServer3.EntityFrameworkCore.Entities
{
    public class ClientScope
    {
        public virtual int Id { get; set; }

        public virtual string Scope { get; set; }

        public virtual Client Client { get; set; }
    }
}