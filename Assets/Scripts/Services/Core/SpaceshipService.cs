using Management.Abstraction;
using Management.Spaceship;
using Services.Abstraction.Spaceship;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using Services.PoolSystem.Abstaction;
using System;
using UnityEngine;

namespace Services.Core
{
    public class SpaceshipService : ISpaceshipService
    {
        private IPoolService _poolService;
        private GameObject _player;
        private GameObject _enemy;

        public ISpaceshipController Player => _player.GetComponent<ISpaceshipController>();

        public SpaceshipService(IPoolService poolService, IEventService eventService)
        {
            _poolService = poolService;
            eventService.RegisterEvent(EventTypes.OnLevelStarted, LevelStarted);
            eventService.RegisterEvent(EventTypes.OnEnemySpaceshipDestoyed, EnemySpaceshipDestoyed);
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

        public ISpaceshipController CreateEnemy(Vector3 position)
        {
            _enemy = _poolService.GetGameObject(EnemyController.POOL_NAME);
            _enemy.transform.position = position;
            return _enemy.GetComponent<ISpaceshipController>();
        }

        public void RemoveEnemy(GameObject enemy)
        {
            _poolService.ReleaseGameObject(enemy);
        }

        private void LevelStarted()
        {
            if (_enemy != null)
            {
                RemoveEnemy(_enemy);
            }
        }

        private void EnemySpaceshipDestoyed()
        {
            _enemy = null;
        }
    }
}