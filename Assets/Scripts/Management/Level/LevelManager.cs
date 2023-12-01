using Common;
using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.Spaceship;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Management.Level
{
    public class LevelManager : MonoBehaviour
    {
        private ISpaceshipService _spaceshipService;

        private void Awake()
        {
            _spaceshipService = ServiceHolder.ServiceProvider.GetService<ISpaceshipService>();
        }

        private void Start()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            _spaceshipService.CreatePlayer();
        }
    }
}