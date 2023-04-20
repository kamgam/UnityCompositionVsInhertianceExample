using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.Composition.Phase1
{
    // In a perfect world Unity would support this and ask which implementation to add.
    // [RequireComponent(typeof(IDamageDealer), typeof(IDamageReceiver))]

    // We chose a concrete implementation here.
    [RequireComponent(typeof(DamageDealer), typeof(DamageReceiver))]
    [SelectionBase]
    public class Player : MonoBehaviour
    {
        // In real game this would be a reference to an item system returning a damage value.
        // If you set this to negative it will be healing.
        public int DamageValue = 10;

        // In real game this would be a reference to an item system returning a range value.
        [Min(0f)]
        public float DamageEffectRange = 5f;

        protected IDamageDealer _damageDealer;
        public IDamageDealer DamageDealer
        {
            get
            {
                if (_damageDealer == null)
                {
                    _damageDealer = this.GetComponent<IDamageDealer>();
                }
                return _damageDealer;
            }
        }

        protected IDamageReceiver damageReceiver;
        public IDamageReceiver DamageReceiver
        {
            get
            {
                if (damageReceiver == null)
                {
                    damageReceiver = this.GetComponent<IDamageReceiver>();
                }
                return damageReceiver;
            }
        }

        public void Awake()
        {
            DamageReceiver.OnHealthChanged += OnDamageTaken;
        }

        public void OnDamageTaken(IDamageDealer dealer, IDamageReceiver receiver, int damage)
        {
            Debug.Log($"{receiver.gameObject.name} took {damage} from {dealer.gameObject.name} and has {receiver.Health} HP left.");
        }

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

            DamageReceiverRegistry.FindNearby(transform.position, maxDistance: 5, results: _damageReceivers, includeInactive: false, exclude: DamageReceiver);
            DamageDealer.DealDamage(_damageReceivers, DamageValue);
        }
    }
}
