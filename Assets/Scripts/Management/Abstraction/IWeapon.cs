using Services.Data.Abstraction;
using UnityEngine;

namespace Management.Abstraction
{
    public interface IWeapon
    {
        bool Shoot(Vector3 bulletPosition, Vector3 direction);
    }
}