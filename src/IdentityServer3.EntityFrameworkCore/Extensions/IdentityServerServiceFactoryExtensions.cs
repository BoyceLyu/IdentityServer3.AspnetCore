using System;
using IdentityServer3.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer3.EntityFrameworkCore.Extensions
{
    public static class IdentityServerServiceFactoryExtensions
    {
        public static EntityFrameworkOptions ConfigureEntityFramework(this IdentityServerServiceFactory factory, IServiceProvider services)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (services == null) throw new ArgumentNullException("sevices");

            // Assert scope factory is registered
            services.GetRequiredService<IServiceScopeFactory>();
            factory.Register(new Registration<IServiceScopeFactory>(r => services.GetService<IServiceScopeFactory>()));

            return new EntityFrameworkOptions(factory, services);
        }
        
        internal static IdentityServerServiceFactory RegisterScopedAspnetService<T>(this IdentityServerServiceFactory factory)
            where T : class
        {
            return factory.RegisterScopedAspnetService<T, T>();
        }

        internal static IdentityServerServiceFactory RegisterScopedAspnetService<TClass, TAs>(this IdentityServerServiceFactory factory)
            where TClass : TAs
            where TAs : class
        {
            factory.Register(new Registration<TAs>(r => r.Resolve<IServiceScopeFactory>().GetScopedService<TClass>()));
            return factory;
        }
    }
}