using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.Composition.Phase2
{
    public class DamageReceiver : MonoBehaviour, IDamageReceiver
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

        public void TakeDamage(IDamageDealer dealer, int damage)
        {
            Health = Mathf.Clamp(Health - damage, MinHealth, MaxHealth);

            OnHealthChanged?.Invoke(dealer, this, damage);
            OnHealthChangedEvent?.Invoke(dealer, this, damage);
        }

        public void Awake()
        {
            DamageReceiverRegistry.Add(this);
        }
    }
}
