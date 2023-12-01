using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.IntegrationTests
{
    public class PlayerShootsAsteroidTest
    {
        [UnityTest]
        public IEnumerator TestWeaponShootingToAsteroid()
        {
            IWeaponService weaponService = ServiceHolder.ServiceProvider.GetService<IWeaponService>();
            IAsteroidsService asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();

            IWeapon weapon = weaponService.GetWeapon(WeaponType.Pistol);
            IAsteroid asteroid = asteroidsService.AddAsteroid(AsteroidType.Large);
            asteroid.Stop();
            asteroid.SetPosition(Vector2.right * 3);

            float beforeShootingHealth = ((IDamageable)asteroid).Health;
            weapon.Shoot(Vector3.zero, Vector3.right);

            yield return new WaitForSeconds(2);

            Assert.AreNotEqual(beforeShootingHealth, ((IDamageable)asteroid).Health);
        }

        [UnityTearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}