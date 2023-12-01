using Management.Abstraction;
using Management.Asteroid;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
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

            float dieNeededDamage = largeAsteroid.Health;
            isDead = largeAsteroid.TakeDamage(dieNeededDamage);
            Assert.IsTrue(isDead);
        }
        
        [Test]
        public void TestLargeAsteroidBreakDown()
        {
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            IDamageable largeAsteroid = asteroidsService.AddAsteroid(AsteroidType.Large) as IDamageable;

            float dieNeededDamage = largeAsteroid.Health;
            largeAsteroid.TakeDamage(dieNeededDamage);

            MediumAsteroid[] mediumAsteroids = GameObject.FindObjectsOfType<MediumAsteroid>();
            Assert.IsTrue(mediumAsteroids.Length > 0);
        }
        
        [Test]
        public void TestMediumAsteroidBreakDown()
        {
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            IDamageable mediumAsteroid = asteroidsService.AddAsteroid(AsteroidType.Medium) as IDamageable;

            float dieNeededDamage = mediumAsteroid.Health;
            mediumAsteroid.TakeDamage(dieNeededDamage);

            SmallAsteroid[] smallAsteroids = GameObject.FindObjectsOfType<SmallAsteroid>();
            Assert.IsTrue(smallAsteroids.Length > 0);
        }
        
        [Test]
        public void TestSmallAsteroidBreakDown()
        {
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            IDamageable smallAsteroid = asteroidsService.AddAsteroid(AsteroidType.Small) as IDamageable;

            float dieNeededDamage = smallAsteroid.Health;
            smallAsteroid.TakeDamage(dieNeededDamage);

            SmallAsteroid[] smallAsteroids = GameObject.FindObjectsOfType<SmallAsteroid>();
            Assert.IsTrue(smallAsteroids.Length == 0);
        }

        [TearDown]
        public void Cleanup()
        {
            DeleteAllObjects();
            ServiceHolder.Dispose();
        }

        private void DeleteAllObjects()
        {
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                GameObject.DestroyImmediate(o);
            }

        }
    }
}