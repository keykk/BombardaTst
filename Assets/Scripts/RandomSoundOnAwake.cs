using System.Collections.Generic;
using UnityEngine;

public class RandomSoundOnAwake : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioSource thisAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       thisAudioSource = GetComponent<AudioSource>(); 
    }
    void Start()
    {
        if (thisAudioSource != null && audioClips.Count > 0)
        {
            AudioClip audioClip = audioClips[Random.Range(0, audioClips.Count)];
            thisAudioSource.PlayOneShot(audioClip);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
