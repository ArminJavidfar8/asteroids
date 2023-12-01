using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.Abstraction;
using Services.Data.Abstraction;

namespace Tests.EditMode.UnitTest
{
    public class WeaponServiceTest
    {
        [Test]
        public void TestReadingWeaponsData()
        {
            IWeaponService weaponService = ServiceHolder.ServiceProvider.GetService<IWeaponService>();
            IWeaponData[] weaponsData = weaponService.ReadWeaponsData();

            Assert.IsNotNull(weaponsData);
            Assert.IsTrue(weaponsData.Length > 0);
        }

        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}