using Services.Abstraction;
using Services.Data.Abstraction;
using UnityEngine;

namespace Services.Data
{
    [CreateAssetMenu(fileName = "AsteroidData", menuName = "Asteroids/AsteroidData")]
    public class AsteroidData : ScriptableObject, IAsteroidData
    {
        [SerializeField] private AsteroidType _asteroidType;
        [SerializeField] private int _moveForce;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _breakDownScore;

        public AsteroidType AsteroidType => _asteroidType;
        public int MaxHealth => _maxHealth;
        public int BreakDownScore => _breakDownScore;
        public int MoveForce => _moveForce;
    }
}