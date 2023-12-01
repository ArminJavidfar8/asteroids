using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.EditMode.UnitTest
{
    public class AsteroidDamageTest
    {
        [Test]
        public void TestAsteroidDamage()
        {
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            IDamageable largeAsteroid = asteroidsService.AddAsteroid(AsteroidType.Large) as IDamageable;

            float beforeDamageHealth = largeAsteroid.Health;
            Assert.IsTrue(beforeDamageHealth > 0);

            bool isDead = largeAsteroid.TakeDamage(5);
            Assert.IsFalse(isDead);
            Assert.AreEqual(beforeDamageHealth - 5, largeAsteroid.Health);

            isDead = largeAsteroid.TakeDamage(200);
            Assert.IsTrue(isDead);
        }

        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}