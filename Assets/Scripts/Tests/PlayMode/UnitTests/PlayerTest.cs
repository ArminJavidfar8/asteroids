using Management.Abstraction;
using Management.Core;
using Management.Spaceship;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services.PoolSystem.Abstaction;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode.UnitTest
{
    public class PlayerTest
    {
        [UnityTest]
        public IEnumerator TestPlayerMovement()
        {
            IPoolService poolService = ServiceHolder.ServiceProvider.GetService<IPoolService>();
            GameObject player = poolService.GetGameObject(PlayerController.POOL_NAME);
            
            Vector3 beforeMovementPosition = player.transform.position;

            ISpaceshipController playerShip = player.GetComponent<SpaceshipController>();

            playerShip.Move(Vector3.right);

            yield return 0;

            Assert.AreNotEqual(player.transform.position, beforeMovementPosition);
        }
        
        [UnityTest]
        public IEnumerator TestPlayerRotation()
        {
            IPoolService poolService = ServiceHolder.ServiceProvider.GetService<IPoolService>();
            GameObject player = poolService.GetGameObject(PlayerController.POOL_NAME);
            
            float beforeRotatingRotation = player.transform.rotation.eulerAngles.z;
            Debug.Log(beforeRotatingRotation);
            ISpaceshipController playerShip = player.GetComponent<SpaceshipController>();

            playerShip.Rotate(Vector2.right);

            yield return new WaitForSeconds(0.2f);
            Assert.AreNotEqual(player.transform.rotation.eulerAngles.z, beforeRotatingRotation);
        }

        [UnityTearDown]
        public void Cleanup()
        {
            ServiceHolder.Dispose();
        }
    }
}