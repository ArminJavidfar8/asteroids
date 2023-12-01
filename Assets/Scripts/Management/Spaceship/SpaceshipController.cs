using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.Spaceship;
using Services.Data;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using UnityEngine;

namespace Management.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceshipController : MonoBehaviour, ISpaceshipController
    {
        [SerializeField] private Transform _weaponPosition;
        private Rigidbody2D _rigidbody;
        private int _forwardMotorPower;
        private int _rotationPower;
        private Transform _transform;

        public Vector3 WeaponPosition => _weaponPosition.position;
        public Vector3 Up => _transform.up;
        public Vector3 Position => _transform.position;

        public void Initialize(int forwardMotorPower, int rotationPower)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _forwardMotorPower = forwardMotorPower;
            _rotationPower = rotationPower;
            _transform = transform;
        }

        public void OnGetFromPool() { }

        public void OnReleaseToPool() { }

        public void Move(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _forwardMotorPower * Time.deltaTime);
        }

        public void Rotate(Vector2 input)
        {
            _rigidbody.AddTorque(-input.x * _rotationPower * Time.deltaTime);
        }
    }
}