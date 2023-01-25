using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;
    public AudioSource audioSourceSE;
    public AudioClip[] audioClips;
    public AudioClip[] pianos;
    

    private void Awake()
    {
        if(i == null)
        {
            i = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlayWalk()
    {
        audioSourceSE.PlayOneShot(audioClips[3]);
    }

    public void PlayButton()
    {
        audioSourceSE.PlayOneShot(audioClips[0]);
    }
    public void PlayEvent()
    {
        audioSourceSE.PlayOneShot(audioClips[1]);
    }
    public void PlayDoor()
    {
        audioSourceSE.PlayOneShot(audioClips[2]);
    }

    public void PlayTitle()
    {
        audioSourceSE.PlayOneShot(audioClips[4]);
    }

    public void PlayCandleOn()
    {
        audioSourceSE.PlayOneShot(audioClips[5]);
    }

    public void PlayEvent_Short()
    {
        audioSourceSE.PlayOneShot(audioClips[7]);
    }

    public void PlayThunder()
    {
        audioSourceSE.PlayOneShot(audioClips[6]);
    }

    public void PlayEnding()
    {
        audioSourceSE.clip = audioClips[8];
        audioSourceSE.Play();
    }

    public void PlayPiano(int idx)
    {
        
        audioSourceSE.PlayOneShot(pianos[idx]);
    }


}
