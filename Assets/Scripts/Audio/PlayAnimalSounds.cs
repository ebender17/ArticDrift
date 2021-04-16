using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to animal and trigger methods using animation events.
/// </summary>
public class PlayAnimalSounds : MonoBehaviour
{
    [Header("Broadcasting on")]
    [SerializeField] private AudioSoundEventChannelSO _playSFXChannel;
    [SerializeField] private AnimalSounds _animalSounds;

    public void PlayIdle()
    {
        _playSFXChannel.RaiseEvent(_animalSounds.idle, transform.position);
    }

    public void PlayWalking()
    {
        _playSFXChannel.RaiseEvent(_animalSounds.walking, transform.position);
    }

    public void PlayEating()
    {
        _playSFXChannel.RaiseEvent(_animalSounds.eating, transform.position);
    }

  
}
