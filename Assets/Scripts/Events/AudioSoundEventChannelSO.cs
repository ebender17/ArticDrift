using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioSoundEventChannel", menuName = "Events/Audio Sound Event Channel")]
public class AudioSoundEventChannelSO : EventChannelBaseSO
{
    public AudioSoundEventAction OnEventRaised;

    public void RaiseEvent(Sound sound, Vector3 pos)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(sound, pos);
    }
}

public delegate void AudioSoundEventAction(Sound sound, Vector3 pos);
