using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    const float defaultVolume = 0.8f;

    private void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = defaultVolume;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
