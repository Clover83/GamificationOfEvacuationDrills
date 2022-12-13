using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sound manager script
//Can be expanded upon for background music etc.
public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource, _sfxSource;

    //Singleton
    public static SoundHandler Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //Don't destroy it when loading a new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
