using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.Composition.Phase2
{
    public interface IDamageDealer
    {
        GameObject gameObject { get; }

        void DealDamage(IEnumerable<IDamageReceiver> receivers, int value);
        void DealDamage(IDamageReceiver receiver, int value);
    }
}
