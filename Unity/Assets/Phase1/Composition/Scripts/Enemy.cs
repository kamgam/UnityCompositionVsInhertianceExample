using UnityEngine;

namespace Kamgam.Composition.Phase1
{
    [SelectionBase]
    // We chose a concrete implementation here.
    [RequireComponent(typeof(DamageDealer), typeof(DamageReceiver))]
    public class Enemy : MonoBehaviour
    {
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
    }
}
