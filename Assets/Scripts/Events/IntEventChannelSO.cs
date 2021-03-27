using UnityEngine;

[CreateAssetMenu(fileName = "NewIntEventChannel", menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : ScriptableObject
{
    public IntEventAction OnEventRaised;

    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }

}

public delegate void IntEventAction(int value);
