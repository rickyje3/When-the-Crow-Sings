using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScrObj
{
    [CreateAssetMenu]
    public class GameSignal : ScriptableObject
    {
        List<GameSignalListener> listeners = new List<GameSignalListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnSignalRaised(); // Useful in case the response involves removing it from the list.
            }
        }

        public void Raise(float delay_in_seconds)
        {
            // TODO: Use the delay_in_seconds.
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnSignalRaised(); // Useful in case the response involves removing it from the list.
            }
        }

        public void RegisterListener(GameSignalListener listener)
        {
            // Should add to the list
            listeners.Add(listener);
        }
        public void UnregisterListener(GameSignalListener listener)
        {
            // Should remove from the list
            listeners.Remove(listener);
        }

    }
}
