using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management.Abstraction
{
    public interface IDamageableDeathListener
    {
        void OnDamageableDied(IDamageable damageable);
    }
}