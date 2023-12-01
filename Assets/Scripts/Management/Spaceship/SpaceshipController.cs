using Management.Abstraction;
using Services.Data;
using UnityEngine;

namespace Management.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipController : MonoBehaviour, ISpaceshipController
    {
        [SerializeField] private SpaceshipData _spaceshipData;
        [SerializeField] private Transform _weaponPosition;
        private Rigidbody2D _rigidbody;
        private int _forwardMotorPower;
        private int _rotationPower;
        private Transform _transform;

        public Vector3 WeaponPosition => _weaponPosition.position;
        public Vector3 Up => _transform.up;

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _forwardMotorPower = _spaceshipData.ForwardMotorPower;
            _rotationPower = _spaceshipData.RotationPower;
            _transform = transform;
        }

        public void MoveForward()
        {
            _rigidbody.AddForce(_transform.up * _forwardMotorPower * Time.deltaTime);
        }

        public void Rotate(Vector2 input)
        {
            _rigidbody.AddTorque(-input.x * _rotationPower * Time.deltaTime);
        }
    }
}