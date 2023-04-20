using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.Inheritance.Phase1
{
    [SelectionBase]
    public class Player : Enemy
    {
        // In real game this would be a reference to an item system returning a damage value.
        // If you set this to negative it will be healing.
        public int DamageValue = 10;

        // In real game this would be a reference to an item system returning a range value.
        [Min(0f)]
        public float DamageEffectRange = 5f;

        public void Start()
        {
            // For demo only. Don't use strings as references in production.
            InvokeRepeating("DamageAllNearby", 0.5f, 2f);
        }

        // The list of damage receivers the player is currently hitting.
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
