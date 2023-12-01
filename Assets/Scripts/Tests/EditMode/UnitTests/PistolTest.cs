using Management.Abstraction;
using Management.Core;
using Management.Weapon;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.UnitTest
{
    public class PistolTest
    {
        [Test]
        public void TestShoot()
        {
            IWeapon pistol = new Pistol(0);
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