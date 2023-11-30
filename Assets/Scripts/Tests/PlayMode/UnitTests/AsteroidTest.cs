using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
using System;
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

            Array allAsteroidTypes = Enum.GetValues(typeof(AsteroidType));
            for (int i = 1; i < allAsteroidTypes.Length; i++)
            {
                IAsteroid asteroid = asteroidsService.AddAsteroid((AsteroidType)allAsteroidTypes.GetValue(i));

                Vector3 beforeMovementPosition = asteroid.Position;
                asteroid.MoveProperly();

                yield return new WaitForSeconds(0.2f);

                Assert.AreNotEqual(beforeMovementPosition, asteroid.Position);
            }
        }

        [UnityTearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}