using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.Data;
using Services.Data.Abstraction;
using UnityEngine;

namespace Management.Asteroid
{
    public class Asteroid : MonoBehaviour, IAsteroid
    {
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private IAsteroidsService _asteroidsService;
        private Transform _transform;

        public string Name => _asteroidData.AsteroidType.ToString();
        public IAsteroidData AsteroidData => _asteroidData;
        public GameObject TheGameObject => gameObject;

        public void Initialize()
        {
            _transform = transform;
            _asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
        }

        public void OnGetFromPool()
        {
            SetSprite();
        }

        public void OnReleaseToPool()
        {
            
        }

        public void StartMoving()
        {
            
        }

        public void BreakDown()
        {

        }

        private void SetSprite()
        {
            _spriteRenderer.sprite = _asteroidsService.GetRandomAsteroidSprite(AsteroidData.AsteroidType);
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }
    }
}