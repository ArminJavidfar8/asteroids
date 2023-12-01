using Services.Abstraction;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core
{
    public class LevelService : ILevelService
    {
        private readonly IEventService _eventService;
        private int _score;
        private int _currentLevel;

        public int CurrentLevel => _currentLevel;
        public int TotalAsteroids => 5; // read this value from a scriptable object 
        public float AsteroidInstantiationRatio => 1; // read this value from a scriptable object 
        public int LevelSize => 6; // read this value from a scriptable object 
        public bool HasEnemySpaceship => true; // read this value from a scriptable object 
        private int Score { get => _score; 
            set 
            {  
                _score = value;
                _eventService.BroadcastEvent(EventTypes.OnScoreUpdated, _score);
            } 
        }

        public LevelService(IEventService eventService)
        {
            _eventService = eventService;
            _eventService.RegisterEvent(EventTypes.OnLevelStarted, LevelStarted);
            _eventService.RegisterEvent<int>(EventTypes.OnEnemyDied, EnemyDied);
        }

        private void EnemyDied(int score)
        {
            Score += score;
        }

        private void LevelStarted()
        {
            Score = 0;
        }
    }
}