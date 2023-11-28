using Microsoft.Extensions.DependencyInjection;
using Services.CoroutineSystem.Abstractio;
using Services.CoroutineSystem.Core;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Core;
using Services.PoolSystem.Abstaction;
using Services.PoolSystem.Core;
using System;

namespace Management.Core
{
    public class ServiceHolder
    {
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    ServiceCollection serviceCollection = new ServiceCollection();
                    serviceCollection.AddSingleton<IPoolService, PoolService>();
                    serviceCollection.AddSingleton<IEventService, EventService>();
                    serviceCollection.AddSingleton<ICoroutineService, CoroutineService>();

                    _serviceProvider = serviceCollection.BuildServiceProvider();

                    // These lines won't be needed by using a dependency injection library
                    _ = _serviceProvider.GetService<IPoolService>();
                    _ = _serviceProvider.GetService<IEventService>();
                    _ = _serviceProvider.GetService<ICoroutineService>();
                }
                return _serviceProvider;
            }
        }
    }
}