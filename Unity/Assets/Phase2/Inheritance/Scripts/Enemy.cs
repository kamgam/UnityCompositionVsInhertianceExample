using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.Inheritance.Phase2
{
    [SelectionBase]
    public class Enemy : MonoBehaviour, IDamageDealer, IDamageReceiver
    {
        public int MinHealth = 0;
        public int MaxHealth = 100;

        [SerializeField]
        protected int _health = 100;
        public int Health
        {
            get => _health;
            set => _health = value;
        }

        // Events, we suport both Unity and pure c# events.
        public event Action<IDamageDealer, IDamageReceiver, int> OnHealthChanged;

        public UnityEvent<IDamageDealer, IDamageReceiver, int> _onHealthChangedEvent;
        public UnityEvent<IDamageDealer, IDamageReceiver, int> OnHealthChangedEvent
        {
            get => _onHealthChangedEvent;
        }

        public virtual void Awake()
        {
            DamageReceiverRegistry.Add(this);
        }

        public void TakeDamage(IDamageDealer dealer, int damage)
        {
            Health = Mathf.Clamp(Health - damage, MinHealth, MaxHealth);

            OnHealthChanged?.Invoke(dealer, this, damage);
            OnHealthChangedEvent?.Invoke(dealer, this, damage);

            Debug.Log($"{gameObject.name} took {damage} from {dealer.gameObject.name} and has {Health} HP left.");
        }

        public void DealDamage(IEnumerable<IDamageReceiver> receivers, int damage)
        {
            foreach (var receiver in receivers)
            {
                DealDamage(receiver, damage);
            }
        }

        public void DealDamage(IDamageReceiver receiver, int damage)
        {
            receiver.TakeDamage(this, damage);
        }
    }
}
