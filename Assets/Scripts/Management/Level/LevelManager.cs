using Common;
using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Management.Level
{
    public class LevelManager : MonoBehaviour
    {
        private IPoolService _poolService;

        private void Awake()
        {
            _poolService = ServiceHolder.ServiceProvider.GetService<IPoolService>();
        }

        private void Start()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            _poolService.GetGameObject(Spaceship.PlayerController.POOL_NAME);
        }
    }
}