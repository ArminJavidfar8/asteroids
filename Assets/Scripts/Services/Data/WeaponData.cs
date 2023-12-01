using Services.Abstraction;
using Services.Data.Abstraction;
using UnityEngine;

namespace Services.Data
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Asteroids/Weapon")]
    public class WeaponData : ScriptableObject, IWeaponData
    {
        [SerializeField] private string _name;
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private int _damage;
        [SerializeField] private float _fireRate;

        public string Name => _name;
        public WeaponType WeaponType => _weaponType;
        public int Damage => _damage;
        public float FireRate => _fireRate;
    }
}