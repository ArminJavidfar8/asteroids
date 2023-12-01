using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using Services.PoolSystem.Abstaction;
using System;
using UnityEngine;

namespace Management.Spaceship
{
    [RequireComponent(typeof(ISpaceshipController))]
    public class PlayerController : MonoBehaviour, IPoolable
    {
        public const string POOL_NAME = "PlayerSpaceship";

        private IEventService _eventService;
        private ISpaceshipController _spaceshipController;
        private IWeapon _weapon;

        public string Name => POOL_NAME;

        public void Initialize()
        {
            IWeaponService weaponService = ServiceHolder.ServiceProvider.GetService<IWeaponService>();
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>(); // this can be filled with a DI library
            _spaceshipController = GetComponent<ISpaceshipController>();
            _spaceshipController.Initialize();

            _weapon = weaponService.GetWeapon(WeaponType.Pistol);
        }

        public void OnGetFromPool()
        {
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
            _spaceshipController.MoveForward();
        }

        private void UserClickedSides(Vector2 input)
        {
            _spaceshipController.Rotate(input);
        }

        private void UserShot()
        {
            _weapon.Shoot(_spaceshipController.WeaponPosition, _spaceshipController.Up);
        }
    }
}