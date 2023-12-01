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
    public class SpaceshipController : MonoBehaviour, ISpaceshipController, IDamageable
    {
        [SerializeField] private SpaceshipData _spaceshipData;
        [SerializeField] private Transform _weaponPosition;
        private IEventService _eventService;
        private ISpaceshipService _spaceshipService;
        private Rigidbody2D _rigidbody;
        private int _forwardMotorPower;
        private int _rotationPower;
        private Transform _transform;
        private float _health;

        public Vector3 WeaponPosition => _weaponPosition.position;
        public Vector3 Up => _transform.up;
        public float MaxHealth => _spaceshipData.MaxHealth;
        public float Health { get => _health; set => _health = value; }
        public bool IsAlive => Health > 0;

        public void Initialize()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
            _spaceshipService = ServiceHolder.ServiceProvider.GetService<ISpaceshipService>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _forwardMotorPower = _spaceshipData.ForwardMotorPower;
            _rotationPower = _spaceshipData.RotationPower;
            _transform = transform;
        }

        public void OnGetFromPool()
        {
            Health = MaxHealth;
        }

        public void OnReleaseToPool()
        {
            
        }

        public void MoveForward()
        {
            _rigidbody.AddForce(_transform.up * _forwardMotorPower * Time.deltaTime);
        }

        public void Rotate(Vector2 input)
        {
            _rigidbody.AddTorque(-input.x * _rotationPower * Time.deltaTime);
        }

        public bool TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            _eventService.BroadcastEvent(EventTypes.OnPlayerDied);
            _spaceshipService.RemovePlayer();
        }
    }
}