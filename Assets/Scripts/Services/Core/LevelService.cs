using Common;
using Services.Abstraction;
using Services.CoroutineSystem.Abstractio;
using Services.Data.Abstraction;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Services.Core
{
    public class LevelService : ILevelService
    {
        private readonly IEventService _eventService;
        private readonly ILevelData[] _levelsData;
        private int _score;
        private int _currentLevelIndex;
        private int _levelTotalAsteroids;
        private int _destroyedAsteroids;

        public int CurrentLevelNumber => _currentLevelIndex + 1;
        public ILevelData CurrentLevelData => _levelsData[_currentLevelIndex];
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
            _eventService.RegisterEvent<int>(EventTypes.OnAstroidDestroyed, AstroidDestroyed);
            _eventService.RegisterEvent<int>(EventTypes.OnEnemySpaceshipDestoyed, EnemySpaceshipDestoyed);
            _eventService.RegisterEvent<bool>(EventTypes.OnLevelFinished, LevelFinished);
            _levelsData = ReadLevelsData();
        }

        public ILevelData[] ReadLevelsData()
        {
            object[] levelsObject = Resources.LoadAll(Constants.Paths.Levels);
            int length = levelsObject.Length;
            ILevelData[] levels = new ILevelData[length];
            for (int i = 0; i < length; i++)
            {
                levels[i] = levelsObject[i] as ILevelData;
            }
            return levels;
        }


        public int CurrentLevelTotalAstroids()
        {
            return CalculateTotalAstroids(CurrentLevelData.LargeAsteroids, CurrentLevelData.MediumAsteroids, CurrentLevelData.SmallAsteroids);
        }

        public int CalculateTotalAstroids(int largeNumbers, int mediumNumbers, int smallNumbers)
        {
            int totalLarge = largeNumbers;
            int totalMedium = mediumNumbers + totalLarge * 2; // large generates 2 mediums
            int totalSmall = smallNumbers + totalMedium * 3; // each medium generates 3 smalls
            return totalLarge + totalMedium + totalSmall;
        }

        private void AstroidDestroyed(int score)
        {
            Score += score;
            ++_destroyedAsteroids;
            CheckForWinCondition();
        }

        private void EnemySpaceshipDestoyed(int score)
        {
            Score += score;
        }

        private void CheckForWinCondition()
        {
            if (_destroyedAsteroids == _levelTotalAsteroids)
            {
                ++_currentLevelIndex;
                _eventService.BroadcastEvent(EventTypes.OnLevelFinished, true);
            }
        }

        private void LevelStarted()
        {
            _destroyedAsteroids = 0;
            _levelTotalAsteroids = CalculateTotalAstroids(CurrentLevelData.LargeAsteroids, CurrentLevelData.MediumAsteroids, CurrentLevelData.SmallAsteroids);
            Score = 0;
        }

        private void LevelFinished(bool won)
        {
            if (!won)
            {
                _currentLevelIndex = 0;
            }
        }

    }
}