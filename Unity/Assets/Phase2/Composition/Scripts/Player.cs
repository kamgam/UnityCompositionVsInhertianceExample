using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.Composition.Phase2
{
    [RequireComponent(typeof(DamageDealer), typeof(DamageReceiver))]
    [SelectionBase]
    public class Player : MonoBehaviour
    {
        public int DamageValue = 10;

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
            InvokeRepeating("DamageAllNearby", 0.5f, 2f);
        }

        protected List<IDamageReceiver> _damageReceivers = new List<IDamageReceiver>();

        public void DamageAllNearby()
        {
            Debug.Log($"{gameObject.name} is dealing damage.");

            DamageReceiverRegistry.FindNearby(transform.position, maxDistance: 5, results: _damageReceivers, includeInactive: false, exclude: DamageReceiver);
            DamageDealer.DealDamage(_damageReceivers, DamageValue);
        }
    }
}
