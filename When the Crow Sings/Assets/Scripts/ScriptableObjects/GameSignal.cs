using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class GameSignal : ScriptableObject
    {
        List<GameSignalListener> listeners = new List<GameSignalListener>();

        public void Emit()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnSignalEmitted(); // Useful in case the response involves removing it from the list.
            }
        }

        public void Emit(float delay_in_seconds)
        {
            // TODO: Use the delay_in_seconds.
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnSignalEmitted(); // Useful in case the response involves removing it from the list.
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
