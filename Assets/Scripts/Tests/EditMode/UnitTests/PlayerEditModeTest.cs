using Management.Core;
using Management.Spaceship;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Tests.EditMode.UnitTest
{
    public class PlayerEditModeTest
    {
        [Test]
        public void TestPlayerInstantiation()
        {
            IPoolService poolService = ServiceHolder.ServiceProvider.GetService<IPoolService>();
            poolService.GetGameObject(PlayerController.POOL_NAME);

            PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
            Assert.IsNotNull(playerController);
        }

        [TearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}