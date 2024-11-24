using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioClipList soundList;
    private int lengthOfList;
    public AudioSource playerAudioSource;

    private void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound(AudioClipList sound)
    {
        lengthOfList = Random.Range(0,sound.AudioList.Count);
        playerAudioSource.PlayOneShot(soundList.AudioList[lengthOfList]);
    }
}
