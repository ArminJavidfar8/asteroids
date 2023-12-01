using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction.Spaceship;
using UnityEngine;

namespace Tests.EditMode.UnitTest
{
    public class EnemyTest : MonoBehaviour
    {
        [Test]
        public void TestEnemyInstantiation()
        {
            ISpaceshipService spaceshipService = ServiceHolder.ServiceProvider.GetService<ISpaceshipService>();

            ISpaceshipController enemy = spaceshipService.CreateEnemy(Vector3.zero);

            Assert.IsNotNull(enemy);
        }
        
        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}