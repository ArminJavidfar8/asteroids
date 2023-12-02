using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;

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
            IAsteroid mediumAsteroid = asteroidsService.AddAsteroid(AsteroidType.Medium);
            Assert.IsNotNull(mediumAsteroid);
            IAsteroid largeAsteroid = asteroidsService.AddAsteroid(AsteroidType.Large);
            Assert.IsNotNull(largeAsteroid);
        }

        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}