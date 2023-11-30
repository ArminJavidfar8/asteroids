using Management.Abstraction;
using Management.Core;
using Management.Spaceship;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
using UnityEngine;

namespace Tests.EditMode.UnitTest
{
    public class AsteroidServiceTest
    {
        [Test]
        public void TestAsteroidInstantiation()
        {
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            IAsteroid smallAsteroid = asteroidsService.AddAsteroid(AsteroidType.Small);

            Assert.IsNotNull(smallAsteroid);
        }
        
        [Test]
        public void TestRandomAsteroid()
        {
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            for (int i = 0; i < 50; i++)
            {
                AsteroidType randomType = asteroidsService.GetRandomAsteroidType();
                Assert.AreNotEqual(randomType, AsteroidType.None);
            }
        }

        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}