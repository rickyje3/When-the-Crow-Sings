
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class GameSignal : ScriptableObject
    {
        public SignalArguments builtinSignalArguments;
        List<GameSignalListener> listeners = new List<GameSignalListener>();

        public void Emit()
        {
            _Emit(builtinSignalArguments);
        }

        public void Emit(float delay_in_seconds)
        {
            // TODO: Use the delay_in_seconds.
            EmitAfterTime(delay_in_seconds);
        }

        public void Emit(SignalArguments externalSignalArguments)
        {
            _Emit(externalSignalArguments);
        }



        private IEnumerator EmitAfterTime(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _Emit(builtinSignalArguments);
        }

        private void _Emit(SignalArguments args)
        {
            //for (int i = listeners.Count - 1; i >= 0; i--)
            //{
            //    args.sender = listeners[i].gameObject;

            //    listeners[i].OnSignalEmitted(args); // Useful in case the response involves removing it from the list.
            //}

            foreach (GameSignalListener i in listeners)
            {
                args.sender = i.gameObject;
                i.OnSignalEmitted(args);
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
