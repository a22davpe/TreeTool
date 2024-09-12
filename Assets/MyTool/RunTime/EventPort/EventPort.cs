using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyTool
{
    [CreateAssetMenu (menuName = "Ports/Event Ports/Event Port")]
    public class EventPort : ScriptableObject
    {
        public UnityAction OnInvoked;

        public void Invoke()
        {
            OnInvoked?.Invoke();
        }
    }

    public class EventPort<T> : ScriptableObject
    {
        public UnityAction<T> OnInvoked;

        public void Invoke(T par1)
        {
            OnInvoked?.Invoke(par1);
        }
    }

    public class EventPort<T1, T2> : ScriptableObject
    {
        public UnityAction<T1, T2> OnInvoked;

        public void Invoke(T1 par1, T2 par2)
        {
            OnInvoked?.Invoke(par1, par2);
        }
    }

    public class IntEventPort : EventPort<int>
    {

    }
}
