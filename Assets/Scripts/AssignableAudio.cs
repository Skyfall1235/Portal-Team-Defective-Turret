using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AssignableAudio : MonoBehaviour
{
    private AudioSource playerAudio;

    public UnityEvent onPlaySoundEvent;

    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        playerAudio.PlayOneShot(sound);
    }
}
