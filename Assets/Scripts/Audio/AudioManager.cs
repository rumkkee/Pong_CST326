using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private AudioSource _paddleHitAudio;
    [SerializeField] private AudioSource _barrierHitAudio;


    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlayClipPaddleHit(float pitch)
    {
        _paddleHitAudio.pitch = pitch;
        _paddleHitAudio.Play();
    }

    public void PlayClipBarrierHit()
    {
        _barrierHitAudio.Play();
    }
}
