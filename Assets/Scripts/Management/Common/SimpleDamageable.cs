using Management.Abstraction;
using UnityEngine;

namespace Management.Common
{
    public class SimpleDamageable : MonoBehaviour, IDamageable
    {
        private IDamageableDeathListener _damageableDeathListener;

        public float MaxHealth { get; private set; }
        public float Health { get; private set; }
        public bool IsAlive => Health > 0;

        public void Setup(IDamageableDeathListener damageableDeathListener, float maxHealth)
        {
            _damageableDeathListener = damageableDeathListener;
            MaxHealth = maxHealth;
        }

        public bool TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            _damageableDeathListener?.OnDamageableDied(this);
        }

    }
}