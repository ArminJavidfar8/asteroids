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
    public class AsteroidServiceTest
    {
        [UnityTest]
        public IEnumerator TestAsteroidsSprite()
        {
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();

            asteroidsService.AddAsteroid(AsteroidType.Small);
            
            asteroidsService.AddAsteroid(AsteroidType.Medium);

            asteroidsService.AddAsteroid(AsteroidType.Large);

            // No assertion, just see and check if asteroids have correct sprite.
            yield return new WaitForSeconds(3);
        }

        [UnityTearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}