using Microsoft.EntityFrameworkCore;

namespace IdentityServer3.EntityFrameworkCore.DbContexts
{
    public abstract class BaseContext : DbContext
    {
        protected BaseContext(DbContextOptions options) 
            : base(options)
        { }
    }
}