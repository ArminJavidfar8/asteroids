using Services.Data.Abstraction;
using UnityEngine;

namespace Management.Abstraction
{
    public interface IBullet
    {
        void SetPosition(Vector3 position);
        void ShootBullet(IWeaponData weaponData, Vector3 direction);
        void ChangeLayer(int layer);
    }
}