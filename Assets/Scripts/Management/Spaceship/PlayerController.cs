using Common;
using Management.Abstraction;
using Management.Common;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.Abstraction.Spaceship;
using Services.Data;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using Services.PoolSystem.Abstaction;
using System;
using TMPro;
using UnityEngine;

namespace Management.Spaceship
{
    [RequireComponent(typeof(ISpaceshipController))]
    public class PlayerController : MonoBehaviour, IPoolable, IDamageableDeathListener
    {
        public const string POOL_NAME = "PlayerSpaceship";

        [SerializeField] private SpaceshipData _spaceshipData;
        [SerializeField] private SimpleDamageable _damageable;

        private IEventService _eventService;
        private ISpaceshipService _spaceshipService;
        private ISpaceshipController _spaceshipController;
        private IWeapon _weapon;
        private Transform _transform;
        private Vector2 _halfscreenWorldSize;

        public string Name => POOL_NAME;

        public void Initialize()
        {
            IWeaponService weaponService = ServiceHolder.ServiceProvider.GetService<IWeaponService>();
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
            _spaceshipService = ServiceHolder.ServiceProvider.GetService<ISpaceshipService>();
            _spaceshipController = GetComponent<ISpaceshipController>();
            _transform = transform;

            _spaceshipController.Initialize(_spaceshipData.ForwardMotorPower, _spaceshipData.RotationPower);
            _weapon = weaponService.GetWeapon(WeaponType.Pistol, LayerMask.NameToLayer(Constants.LayerNames.PLAYER_BULLET));
            CalculateScreenWorldSize();
        }

        private void LateUpdate()
        {
            if(_damageable.IsAlive)
            {
                CheckOutOfScreen();
            }
        }

        public void OnGetFromPool()
        {
            _damageable.Setup(this, _spaceshipData.MaxHealth);
            _eventService.RegisterEvent(EventTypes.OnUserClickedForward, UserClickedForward);
            _eventService.RegisterEvent<Vector2>(EventTypes.OnUserClickedSides, UserClickedSides);
            _eventService.RegisterEvent(EventTypes.OnUserShot, UserShot);
            _spaceshipController.OnGetFromPool();
        }

        public void OnReleaseToPool()
        {
            _eventService.UnRegisterEvent(EventTypes.OnUserClickedForward, UserClickedForward);
            _eventService.UnRegisterEvent<Vector2>(EventTypes.OnUserClickedSides, UserClickedSides);
            _eventService.UnRegisterEvent(EventTypes.OnUserShot, UserShot);
            _spaceshipController.OnReleaseToPool();
        }

        private void UserClickedForward()
        {
            _spaceshipController.Move(_transform.up);
        }

        private void UserClickedSides(Vector2 input)
        {
            _spaceshipController.Rotate(input);
        }

        private void UserShot()
        {
            _weapon.Shoot(_spaceshipController.WeaponPosition, _spaceshipController.Up);
        }

        public void OnDamageableDied(IDamageable damageable)
        {
             _eventService.BroadcastEvent(EventTypes.OnLevelFinished, false);
            _spaceshipService.RemovePlayer();
        }

        private void CalculateScreenWorldSize()
        {
            if (Camera.main != null)
            {
                _halfscreenWorldSize = Camera.main.ScreenToWorldPoint(new(Screen.width, Screen.height));
            }
        }

        private void CheckOutOfScreen()
        {
            Vector3 position = _transform.position;
            if (position.x > _halfscreenWorldSize.x)
            {
                _transform.position = new Vector3(-_halfscreenWorldSize.x, position.y);
            }
            else if (position.x < -_halfscreenWorldSize.x)
            {
                _transform.position = new Vector3(_halfscreenWorldSize.x, position.y);
            }
            if (position.y > _halfscreenWorldSize.y)
            {
                _transform.position = new Vector3(position.x, -_halfscreenWorldSize.y);
            }
            else if (position.y < -_halfscreenWorldSize.y)
            {
                _transform.position = new Vector3(position.x, _halfscreenWorldSize.y);
            }
        }
    }
}