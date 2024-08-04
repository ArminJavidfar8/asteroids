using Management.Abstraction;
using Management.Core;
using Management.Weapon;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Tests.EditMode.UnitTest
{
    public class PistolTest
    {
        [Test]
        public void TestShoot()
        {
            IPoolService poolService = ServiceHolder.ServiceProvider.GetService<IPoolService>();
            IWeaponService weaponService = ServiceHolder.ServiceProvider.GetService<IWeaponService>();
            IWeapon pistol = new Pistol(poolService, weaponService, 0);
            bool shot = pistol.Shoot(Vector3.zero, Vector2.right);
            Assert.IsTrue(shot);
            shot = pistol.Shoot(Vector3.zero, Vector2.right);
            Assert.IsFalse(shot); // it must be false because of fire rate
        }


        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}