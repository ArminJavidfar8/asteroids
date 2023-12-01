using Services.Data.Abstraction;
using UnityEngine;

namespace Services.Data
{
    [CreateAssetMenu(fileName = "SpaceshipData", menuName = "Asteroids/SpaceshipData")]
    public class SpaceshipData : ScriptableObject, ISpaceshipData
    {
        [SerializeField] private string _id;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _forwardMotorPower;
        [SerializeField] private int _rotationPower;
        [SerializeField] private int _killingScore;

        public string Id => _id;
        public int MaxHealth => _maxHealth;
        public int ForwardMotorPower => _forwardMotorPower;
        public int RotationPower => _rotationPower;
        public int KillingScore => _killingScore;
    }
}