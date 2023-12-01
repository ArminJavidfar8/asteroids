using Management.Abstraction;
using Management.Spaceship;
using Services.Abstraction.Spaceship;
using Services.PoolSystem.Abstaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core
{
    public class SpaceshipService : ISpaceshipService
    {
        private IPoolService _poolService;
        private GameObject _player;

        public ISpaceshipController Player => _player.GetComponent<ISpaceshipController>();

        public SpaceshipService(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public ISpaceshipController CreatePlayer()
        {
            _player = _poolService.GetGameObject(PlayerController.POOL_NAME);
            return Player;
        }

        public void RemovePlayer()
        {
            _poolService.ReleaseGameObject(_player);
        }
    }
}