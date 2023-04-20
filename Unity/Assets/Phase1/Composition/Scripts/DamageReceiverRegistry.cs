using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.Composition.Phase1
{
    public static class DamageReceiverRegistry
    {
        static List<IDamageReceiver> _receivers = new List<IDamageReceiver>();

        public static void Add(IDamageReceiver receiver)
        {
            if (!_receivers.Contains(receiver))
            {
                _receivers.Add(receiver);
            }
        }

        public static void Remove(IDamageReceiver receiver)
        {
            _receivers.Remove(receiver);
        }

        public static IEnumerable<IDamageReceiver> FindNearby(
            Vector3 positionInWorldSpace
            , float maxDistance
            , IList<IDamageReceiver> results = null
            , bool includeInactive = false
            , params IDamageReceiver[] exclude)
        {
            if (results == null)
            {
                results = new List<IDamageReceiver>();
            }
            results.Clear();

            float sqrDistance = maxDistance * maxDistance;
            foreach (var receiver in _receivers)
            {
                // Skip if excluded
                for (int i = 0; i < exclude.Length; i++)
                {
                    if(receiver == exclude[i])
                    {
                        goto Skip;
                    }
                }

                // Skip if includeInactive is false and receiver is not active
                if (!includeInactive && !receiver.gameObject.activeInHierarchy)
                {
                    continue;
                }

                // Add only nearby receivers
                if ((receiver.gameObject.transform.position - positionInWorldSpace).sqrMagnitude <= sqrDistance)
                {
                    results.Add(receiver);
                }

                Skip: continue;
            }

            return results;
        }

        
    }
}
