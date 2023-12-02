using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction.Spaceship;

namespace Tests.EditMode.UnitTest
{
    public class PlayerTest
    {
        [Test]
        public void TestPlayerInstantiation()
        {
            ISpaceshipService spaceshipService = ServiceHolder.ServiceProvider.GetService<ISpaceshipService>();
            ISpaceshipController player = spaceshipService.CreatePlayer();

            Assert.IsNotNull(player);
        }

        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}