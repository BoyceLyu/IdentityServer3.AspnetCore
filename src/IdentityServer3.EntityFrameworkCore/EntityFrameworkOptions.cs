using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using System;
using IdentityServer3.EntityFrameworkCore.DbContexts;
using IdentityServer3.EntityFrameworkCore.Extensions;
using IdentityServer3.EntityFrameworkCore.Services;
using IdentityServer3.EntityFrameworkCore.Stores;

namespace IdentityServer3.EntityFrameworkCore
{
    public class EntityFrameworkOptions
    {
        private IdentityServerServiceFactory _factory;
        private IServiceProvider _services;

        public EntityFrameworkOptions(IdentityServerServiceFactory factory, IServiceProvider services)
        {
            _factory = factory;
            _services = services;
        }

        public EntityFrameworkOptions RegisterOperationalStores()
        {
            return RegisterOperationalStores<OperationalContext>();
        }

        public EntityFrameworkOptions RegisterOperationalStores<TOperationalContext>()
            where TOperationalContext : OperationalContext
        {
            _factory.RegisterScopedAspnetService<TOperationalContext, OperationalContext>();

            _factory.AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, AuthorizationCodeStore>();
            _factory.TokenHandleStore = new Registration<ITokenHandleStore, TokenHandleStore>();
            _factory.ConsentStore = new Registration<IConsentStore, ConsentStore>();
            _factory.RefreshTokenStore = new Registration<IRefreshTokenStore, RefreshTokenStore>();

            return this;
        }

        public EntityFrameworkOptions RegisterClientStore< TClientContext>()
            where TClientContext : ClientConfigurationContext
        {
            _factory.RegisterScopedAspnetService<TClientContext, ClientConfigurationContext>();

            _factory.ClientStore = new Registration<IClientStore, ClientStore>();
            _factory.CorsPolicyService = new Registration<ICorsPolicyService, ClientConfigurationCorsPolicyService>();

            return this;
        }

        public EntityFrameworkOptions RegisterScopeStore<TScopeContext>()
            where TScopeContext : ScopeConfigurationContext
        {
            _factory.RegisterScopedAspnetService<TScopeContext, ScopeConfigurationContext>();

            _factory.ScopeStore = new Registration<IScopeStore, ScopeStore>();

            return this;
        }

        /// <summary>
        /// Convienience method to return the IdentityServerServiceFactory.  Not necessary to call but useful for chaining.
        /// </summary>
        /// <returns>The IdentityServerServiceFactory</returns>
        public IdentityServerServiceFactory Build()
        {
            return _factory;
        }
    }
}