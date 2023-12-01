using Common;
using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.Spaceship;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using Services.PoolSystem.Abstaction;
using System;
using UnityEngine;

namespace Management.Level
{
    public class LevelManager : MonoBehaviour
    {
        private ISpaceshipService _spaceshipService;
        private IEventService _eventService;

        private void Awake()
        {
            _spaceshipService = ServiceHolder.ServiceProvider.GetService<ISpaceshipService>();
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void Start()
        {
            _eventService.RegisterEvent(EventTypes.OnLevelStarted, LevelStarted);
        }

        private void LevelStarted()
        {
            _spaceshipService.CreatePlayer();
        }

    }
}