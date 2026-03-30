using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SO_VoidEventChannel", menuName = "Scriptable Objects/SO_VoidEventChannel")]
public class SO_VoidEventChannel : ScriptableObject
{
    public event UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
