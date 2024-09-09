using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    //SoundManager gameobject아래에 bgm, sfx object를 자식으로 둠.
    // 배경음은 거리상관없이 2d사운드식으로
    [SerializeField] AudioSource bgm;
    //레벨업 같은 소리는 2d사운드식으로 거리상관없이 그냥 들리도록하기위해서
    [SerializeField] AudioSource sfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayeBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }

    public void StopBGM()
    {
        if (bgm.isPlaying == false)
        {
            return;
        }
        bgm.Stop();
    }

    public void PauseBGM()
    {
        if (bgm.isPlaying == false)
        {
            return;
        }
        bgm.Pause();
    }

    public void SetBGm(float volume, float pitch = 1f)
    {
        bgm.volume = volume;
        bgm.pitch = pitch;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
        sfx.clip = clip;
        sfx.Play();
    }

    public void SetSFX(float volume, float pitch = 1f)
    {
        sfx.volume = volume;
        sfx.pitch = pitch;
    }
}
