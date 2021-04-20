using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [SerializeField] private AudioSoundEventChannelSO _playMusicEvent = default;
    [SerializeField] private AudioSoundEventChannelSO _playSFXEvent = default;
    [SerializeField] private AudioSoundsEventChannelSO _playSFXRandomEvent = default;

    [SerializeField]
    private bool muteSound;

    [SerializeField]
    private int objectPoolLength = 20;

    [SerializeField]
    private bool logSounds = false;

    private List<AudioSource> pool = new List<AudioSource>();

    private void OnEnable()
    {
        _playMusicEvent.OnEventRaised += PlaySound;
        _playSFXEvent.OnEventRaised += PlaySound;
        _playSFXRandomEvent.OnEventRaised += SelectSound;
    }

    private void OnDisable()
    {
        _playMusicEvent.OnEventRaised -= PlaySound;
        _playSFXEvent.OnEventRaised -= PlaySound;
        _playSFXRandomEvent.OnEventRaised -= SelectSound;
    }

    private void Awake()
    {

        for (int i = 0; i < objectPoolLength; i++)
        {
            GameObject soundObject = new GameObject();
            soundObject.transform.SetParent(transform);
            soundObject.name = "Sound Effect";
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.gameObject.SetActive(false);
            pool.Add(audioSource);
        }
    }

    public void PlaySound(Sound audio, Vector3 pos)
    {
        if (!audio.clip)
        {
            Debug.Log("Clip is null!");
            return;
        }
        
        if(logSounds)
        {
            Debug.Log("Playing Audio: " + audio.name);
        }

        for (int i = 0; i < pool.Count; i++)
        {
            //Picks first audio source not active in the scene
            if (!pool[i].gameObject.activeInHierarchy)
            {
                SetSource(pool[i], audio);

                pool[i].transform.position = pos;
                pool[i].gameObject.SetActive(true);
                pool[i].Play();

                if(!audio.loop)
                    StartCoroutine(ReturnToPool(pool[i].gameObject, audio.clip.length));

                return;
            }
        }

        //If we run out of objects in the pool, create another audio source
        GameObject soundObject = new GameObject();
        soundObject.transform.SetParent(transform);
        soundObject.name = "Sound Effect";

        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        pool.Add(audioSource);

        SetSource(audioSource, audio);

        soundObject.transform.position = pos;
        audioSource.Play();

        if(!audio.loop)
            StartCoroutine(ReturnToPool(soundObject, audio.clip.length));

    }

    private void SetSource(AudioSource source, Sound audio)
    {
        source.clip = audio.clip;
        source.minDistance = audio.soundDistance;
        source.spatialBlend = audio.spatialBlend;
        source.pitch = audio.pitch;
        source.volume = audio.volume;
        source.loop = audio.loop;
        source.outputAudioMixerGroup = audio.audioMixerGroup;

    }

    public void SelectSound(Sound[] clips, Vector3 pos)
    {
        Sound audio = GetRandomClip(clips);
        PlaySound(audio, pos);

        Debug.Log("Select Sound called.");
    }

    private Sound GetRandomClip(Sound[] clips)
    {
        Debug.Log("Get Random Clip called.");
        return clips[UnityEngine.Random.Range(0, clips.Length)]; 
    }

    private IEnumerator ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
