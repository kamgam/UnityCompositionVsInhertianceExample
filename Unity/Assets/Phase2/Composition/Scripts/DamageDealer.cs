using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.Composition.Phase2
{
    public class DamageDealer : MonoBehaviour, IDamageDealer
    {
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
