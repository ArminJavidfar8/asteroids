using Management.Abstraction;
using Services.PoolSystem.Abstaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management.Weapon
{
    public class SimpleBullet : MonoBehaviour, IBullet, IPoolable
    {
        private const float BULLET_SPEED = 300;
        public const string POOL_NAME = "SimpleBullet";
        
        [SerializeField] private Rigidbody2D _rigidbody;
        private Transform _transform;

        public string Name => POOL_NAME;

        public void Initialize()
        {
            _transform = transform;
        }

        public void OnGetFromPool()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        public void OnReleaseToPool()
        {

        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public void MoveTo(Vector3 direction)
        {
            _rigidbody.AddForce(direction * BULLET_SPEED);
        }
    }
}