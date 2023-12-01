using Services.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core
{
    public class LevelService : ILevelService
    {
        private int _currentLevel;

        public int CurrentLevel => _currentLevel;
        public int TotalAsteroids => 5; // read this value from a scriptable object 
        public float AsteroidInstantiationRatio => 1; // read this value from a scriptable object 
        public int LevelSize => 6; // read this value from a scriptable object 
    }
}