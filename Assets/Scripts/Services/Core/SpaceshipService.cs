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
            eventService.RegisterEvent<int>(EventTypes.OnEnemySpaceshipDestoyed, EnemySpaceshipDestoyed);
        }

        public ISpaceshipController CreatePlayer()
        {
            if (_player == null)
            {
                _player = _poolService.GetGameObject(PlayerController.POOL_NAME);
            }
            return Player;
        }

        public void RemovePlayer()
        {
            if (_player != null)
            {
                _poolService.ReleaseGameObject(_player);
                _player = null;
            }
        }

        public ISpaceshipController CreateEnemy(Vector3 position)
        {
            if (_enemy == null)
            {
                _enemy = _poolService.GetGameObject(EnemyController.POOL_NAME);
                _enemy.transform.position = position;
            }
            return _enemy.GetComponent<ISpaceshipController>();
        }

        public void RemoveEnemy(GameObject enemy)
        {
            if (enemy != null)
            {
                _poolService.ReleaseGameObject(enemy);
                _enemy = null;
            }
        }

        private void LevelStarted()
        {
            if (_enemy != null)
            {
                RemoveEnemy(_enemy);
            }
        }

        private void EnemySpaceshipDestoyed(int score)
        {
            _enemy = null;
        }
    }
}