using UnityEngine;

[CreateAssetMenu(fileName = "NewIntEventChannel", menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public VoidEventAction OnEventRaised;
    public void VoidRaiseEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
    
}

public delegate void VoidEventAction();
