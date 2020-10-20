using System;
using JetBrains.Lifetimes;
using Microsoft.Extensions.DependencyInjection;

namespace LifetimeWFPLissue.DI
{
    public static class ServiceProviderExtensions
    {
        public static IServiceScope CreateScope(this IServiceProvider serviceProvider, Lifetime lifetime)
        {
            var scope = serviceProvider.CreateScope();
            var lifeTimeProxy = scope.ServiceProvider.GetRequiredService<LifetimeHolder>();
            lifeTimeProxy.Lifetime = lifetime;

            lifetime.AddDispose(scope);

            return scope;
        }
    }
}