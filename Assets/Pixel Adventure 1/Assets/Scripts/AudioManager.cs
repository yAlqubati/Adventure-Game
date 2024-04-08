using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public void Awake()
    {
        instance = this;
    }

    void start()
    {
        audioSource = GetComponent<AudioSource>();
        FindObjectOfType<AudioManager>();
    }

    public void PlayAudio(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }


}