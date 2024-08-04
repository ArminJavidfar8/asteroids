using Management.AI;
using Services.Abstraction;
using Services.Abstraction.Spaceship;
using Services.Core;
using Services.CoroutineSystem.Abstractio;
using Services.CoroutineSystem.Core;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Core;
using Services.PoolSystem.Abstaction;
using Services.PoolSystem.Core;
using Services.SpriteDatabaseSystem.Abstraction;
using Services.SpriteDatabaseSystem.Core;
using System;
using Microsoft.Extensions.DependencyInjection;
using Management.Level;
using Services.UpdateService.Abstraction;
using Management.UserInput;

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
                    serviceCollection.AddSingleton<IUpdateService, UpdateService>();
                    serviceCollection.AddSingleton<IPoolService, PoolService>();
                    serviceCollection.AddSingleton<IEventService, EventService>();
                    serviceCollection.AddSingleton<ICoroutineService, CoroutineService>();
                    serviceCollection.AddSingleton<IAsteroidsService, AsteroidsService>();
                    serviceCollection.AddSingleton<ISpriteDatabaseService, SpriteDatabaseService>();
                    serviceCollection.AddSingleton<ILevelService, LevelService>();
                    serviceCollection.AddSingleton<EnemyManager>();
                    serviceCollection.AddSingleton<IWeaponService, WeaponService>();
                    serviceCollection.AddSingleton<ISpaceshipService, SpaceshipService>();
                    serviceCollection.AddSingleton<LevelManager>();
                    serviceCollection.AddSingleton<UserInputManager>();

                    _serviceProvider = serviceCollection.BuildServiceProvider();

                    _ = _serviceProvider.GetService<IUpdateService>();
                    _ = _serviceProvider.GetService<IPoolService>();
                    _ = _serviceProvider.GetService<IEventService>();
                    _ = _serviceProvider.GetService<ICoroutineService>();
                    _ = _serviceProvider.GetService<IAsteroidsService>();
                    _ = _serviceProvider.GetService<ISpriteDatabaseService>();
                    _ = _serviceProvider.GetService<ILevelService>();
                    _ = _serviceProvider.GetService<EnemyManager>();
                    _ = _serviceProvider.GetService<IWeaponService>();
                    _ = _serviceProvider.GetService<ISpaceshipService>();
                    _ = _serviceProvider.GetService<LevelManager>();
                    _ = _serviceProvider.GetService<UserInputManager>();
                }
                return _serviceProvider;
            }
        }

        public static void Dispose()
        {
            _serviceProvider = null;
        }
    }
}