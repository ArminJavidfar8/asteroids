using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.Abstraction.Spaceship;
using Services.Data.Abstraction;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Management.Weapon
{
    public class Pistol : IWeapon
    {
        private IPoolService _poolService;
        private IWeaponService _weaponService;
        private IWeaponData _weaponData;
        private float _lastShootTime;

        public Pistol()
        {
            _poolService = ServiceHolder.ServiceProvider.GetService<IPoolService>();
            _weaponService = ServiceHolder.ServiceProvider.GetService<IWeaponService>();

            _weaponData = _weaponService.GetWeaponData(WeaponType.Pistol);

            _lastShootTime = -_weaponData.FireRate; // because player may shoot at the very fist moment of game
        }

        public bool Shoot(Vector3 bulletPosition, Vector3 direction)
        {
            if (CheckFireRate())
            {
                IBullet bullet = _poolService.GetGameObject(SimpleBullet.POOL_NAME).GetComponent<IBullet>();
                bullet.SetPosition(bulletPosition);
                bullet.MoveTo(direction);
                _lastShootTime = Time.time;
                return true;
            }
            return false;
        }

        private bool CheckFireRate()
        {
            return Time.time >= _lastShootTime + _weaponData.FireRate;
        }
    }
}