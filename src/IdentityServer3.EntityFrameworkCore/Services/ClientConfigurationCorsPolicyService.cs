using IdentityServer3.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.EntityFrameworkCore.DbContexts;
using IdentityServer3.EntityFrameworkCore.Extensions;

namespace IdentityServer3.EntityFrameworkCore.Services
{
    public class ClientConfigurationCorsPolicyService : ICorsPolicyService
    {
        private readonly ClientConfigurationContext _context;

        public ClientConfigurationCorsPolicyService(ClientConfigurationContext ctx)
        {
            _context = ctx;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var urls = await _context.Clients
                .SelectMany(x1 => x1.AllowedCorsOrigins).Select(x => x.Origin).ToArrayAsync();

            return urls.Select(x => x.GetOrigin()).Where(x => x != null).Distinct()
                .Contains(origin, StringComparer.OrdinalIgnoreCase);
        }
    }
}