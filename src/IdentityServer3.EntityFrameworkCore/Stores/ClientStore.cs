using IdentityServer3.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using IdentityServer3.EntityFrameworkCore.DbContexts;
using IdentityServer3.EntityFrameworkCore.Entities;
using IdentityServer3.EntityFrameworkCore.Extensions;
using Models = IdentityServer3.Core.Models;

namespace IdentityServer3.EntityFrameworkCore.Stores
{
    public class ClientStore: IClientStore
    {
        private readonly ClientConfigurationContext _context;

        public ClientStore(ClientConfigurationContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this._context = context;
        }

        public async Task<Models.Client> FindClientByIdAsync(string clientId)
        {
            var client = await _context.Clients
                .Include(x => x.ClientSecrets)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.Claims)
                .Include(x => x.AllowedCustomGrantTypes)
                .Include(x => x.AllowedCorsOrigins)
                .SingleOrDefaultAsync(x => x.ClientId == clientId);

            return client.ToModel();
        }
    }
}