﻿using IdentityServer3.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IdentityServer3.EntityFrameworkCore.Extensions
{
    internal static class IServiceProviderExtensions
    {
        private static Registration<T> GetRegistration<T>(this IServiceProvider services) where T : class
        {
            return new Registration<T>(resolver => services.GetRequiredService<T>());
        }

        internal static T GetScopedService<T>(this IServiceScopeFactory factory)
        {
            return factory.CreateScope().ServiceProvider.GetRequiredService<T>();
        }
    }
}