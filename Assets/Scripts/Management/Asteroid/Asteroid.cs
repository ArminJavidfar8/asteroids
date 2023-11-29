using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.Data;
using Services.Data.Abstraction;
using Services.SpriteDatabaseSystem.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management.Asteroid
{
    public class Asteroid : MonoBehaviour, IAsteroid
    {
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private IAsteroidsService _asteroidsService;

        public string Name => _asteroidData.AsteroidType.ToString();
        public IAsteroidData AsteroidData => _asteroidData;

        public void Initialize()
        {
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

    }
}