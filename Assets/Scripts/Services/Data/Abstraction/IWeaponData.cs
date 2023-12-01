using Services.Abstraction;
using UnityEngine;

namespace Services.Data.Abstraction
{
    public interface IWeaponData
    {
        string Name { get; }
        WeaponType WeaponType { get; }
        int Damage { get; }
        float FireRate { get; }
    }
}