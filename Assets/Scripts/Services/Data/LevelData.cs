using Services.Data.Abstraction;
using UnityEngine;

namespace Services.Data
{
    [CreateAssetMenu(fileName = "Level", menuName = "Asteroids/Level")]
    public class LevelData : ScriptableObject, ILevelData
    {
        [SerializeField] private int _smallAsteroids;
        [SerializeField] private int _mediumAsteroids;
        [SerializeField] private int _largeAsteroids;
        [SerializeField] private int _levelSize;
        [SerializeField] private float _asteroidInstantiationRatio;
        [SerializeField] private bool _hasEnemySpaceship;

        public int SmallAsteroids => _smallAsteroids;
        public int MediumAsteroids => _mediumAsteroids;
        public int LargeAsteroids => _largeAsteroids;
        public int LevelSize => _levelSize;
        public float AsteroidInstantiationRatio => _asteroidInstantiationRatio;
        public bool HasEnemySpaceship => _hasEnemySpaceship;

    }
}