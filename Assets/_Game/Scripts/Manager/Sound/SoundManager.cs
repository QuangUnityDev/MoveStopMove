using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public AudioSource audioSourceEffect;
    public AudioClip clipMusic;
    public AudioClip[] clipEffect;

    public void PlaySound()
    {
        audioSource.Play();
    }
    public void PlayEffect()
    {
        //audioSourceEffect.PlayOneShot(cli)
    }
}
