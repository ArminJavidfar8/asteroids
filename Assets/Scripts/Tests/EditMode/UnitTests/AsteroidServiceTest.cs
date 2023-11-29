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

        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}