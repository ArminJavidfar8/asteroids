using Services.Data.Abstraction;
using UnityEngine;

namespace Services.Data
{
    [CreateAssetMenu(fileName = "SpaceshipData", menuName = "Asteroids/SpaceshipData")]
    public class SpaceshipData : ScriptableObject, ISpaceshipData
    {
        [SerializeField] private string _id;
        [SerializeField] private int _forwardMotorPower;
        [SerializeField] private int _rotationPower;

        public string Id => _id;
        public int ForwardMotorPower => _forwardMotorPower;
        public int RotationPower => _rotationPower;
    }
}