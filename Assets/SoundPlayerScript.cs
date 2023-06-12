using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerScript : MonoBehaviour
{
    public AudioSource src;
    public AudioClip flapClip, dieClip, hitClip, scoreClip;

    public void PlayFlapAudio()
    {
        src.PlayOneShot(flapClip);
    }

    public void PlayDieAudio()
    {
        src.PlayOneShot(dieClip);
    }

    public void PlayHitAudio()
    {
        src.PlayOneShot(hitClip);
    }
    public void PlayScoreAudio()
    {
        src.PlayOneShot(scoreClip);
    }
}
