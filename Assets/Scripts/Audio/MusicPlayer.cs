using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in scene to play game music.
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private Sound audioToPlay;

    [SerializeField] private AudioSoundEventChannelSO _playMusicOn = default;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        _playMusicOn.RaiseEvent(audioToPlay, transform.position);
    }
}
