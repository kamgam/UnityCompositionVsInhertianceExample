using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.Composition.Phase2
{
    public interface IDamageReceiver
    {
        GameObject gameObject { get; }

        int Health { get; set; }
        void TakeDamage(IDamageDealer dealer, int value);

        UnityEvent<IDamageDealer, IDamageReceiver, int> OnHealthChangedEvent { get; }
        event System.Action<IDamageDealer, IDamageReceiver, int> OnHealthChanged;
    }
}
