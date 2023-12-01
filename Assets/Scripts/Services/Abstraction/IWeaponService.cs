using Management.Abstraction;
using Services.Data.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction
{
    public enum WeaponType
    {
        None,
        Pistol
    }

    public interface IWeaponService
    {
        IWeaponData[] ReadWeaponsData();
        IWeapon GetWeapon(WeaponType weaponType);
        IWeaponData GetWeaponData(WeaponType weaponType);
    }
}