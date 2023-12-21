using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource, _effectSource;
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

    }
    public void PlaySound(AudioClip clip)
    {
        _effectSource.volume = 1f;
        _effectSource.PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip, bool isBig)
    {
        if (isBig)
        {
            _effectSource.volume = 0.2f;
            _effectSource.PlayOneShot(clip);
        }
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
