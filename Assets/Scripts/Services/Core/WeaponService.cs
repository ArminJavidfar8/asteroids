using Common;
using Management.Abstraction;
using Management.Weapon;
using Services.Abstraction;
using Services.Data.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core
{
    public class WeaponService : IWeaponService
    {
        private IWeaponData[] _weapons;
        public WeaponService()
        {
            _weapons = ReadWeaponsData();
        }

        public IWeaponData[] ReadWeaponsData()
        {
            object[] weaponsObject = Resources.LoadAll(Constants.Paths.WEAPONS);
            int length = weaponsObject.Length;
            IWeaponData[] weapons = new IWeaponData[length];
            for (int i = 0; i < length; i++)
            {
                weapons[i] = weaponsObject[i] as IWeaponData;
            }
            return weapons;
        }

        public IWeapon GetWeapon(WeaponType weaponType, int ownerLayer) => weaponType switch
        {
            WeaponType.Pistol => new Pistol(ownerLayer),
            WeaponType.None or _ => null,
        };

        public IWeaponData GetWeaponData(WeaponType weaponType)
        {
            foreach (IWeaponData weapon in _weapons)
            {
                if (weapon.WeaponType == weaponType)
                {
                    return weapon;
                }
            }
            return null;
        }
    }
}