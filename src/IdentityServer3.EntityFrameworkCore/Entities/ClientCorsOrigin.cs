using System;

namespace IdentityServer3.EntityFrameworkCore.Entities
{
    public class ClientCorsOrigin
    {
        public virtual int Id { get; set; }

        public virtual string Origin { get; set; }

        public virtual Client Client { get; set; }
    }
}