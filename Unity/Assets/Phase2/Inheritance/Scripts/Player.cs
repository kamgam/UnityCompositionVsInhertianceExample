using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.Inheritance.Phase2
{
    [SelectionBase]
    public class Player : EnemyWithMovementSinUpDownAndRotation
    {
        public int DamageValue = 10;

        [Min(0f)]
        public float DamageEffectRange = 5f;

        public void Start()
        {
            InvokeRepeating("DamageAllNearby", 0.5f, 2f);
        }

        protected List<IDamageReceiver> _damageReceivers = new List<IDamageReceiver>();

        public void DamageAllNearby()
        {
            Debug.Log($"{gameObject.name} is dealing damage.");

            DamageReceiverRegistry.FindNearby(transform.position, maxDistance: 5, results: _damageReceivers, includeInactive: false, exclude: this);
            foreach (var receiver in _damageReceivers)
            {
                receiver.TakeDamage(this, DamageValue);
            }
        }
    }
}
