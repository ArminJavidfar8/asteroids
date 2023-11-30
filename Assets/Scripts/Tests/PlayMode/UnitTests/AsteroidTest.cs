using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.UnitTest
{
    public class AsteroidTest
    {
        [UnityTest]
        public IEnumerator TestAsteroidMovement()
        {
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            IAsteroid asteroid = asteroidsService.AddAsteroid(AsteroidType.Small);

            Vector3 beforeMovementPosition = asteroid.Position;
            asteroid.MoveProperly();

            yield return new WaitForSeconds(0.2f);

            Assert.AreNotEqual(beforeMovementPosition, asteroid.Position);
        }

        [UnityTearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}