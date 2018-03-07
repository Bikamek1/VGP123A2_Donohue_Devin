using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


    static SoundManager _instance = null;

    public AudioSource sfxSource;
    public AudioSource musicSource;



    // Use this for initialization
    void Start () {
        if (Instance)
            Destroy(gameObject);
        else
        {
            _instance = this;

            DontDestroyOnLoad(this);
        }
	}
	
    public void PlaySingleSound(AudioClip clip, float volume=1.0f)
    {
        sfxSource.clip = clip;

        sfxSource.volume = volume;

        sfxSource.Play();

    }

    public void PlayMusic(AudioClip clip, float volume = 1.0f)
    {
        musicSource.clip = clip;

        musicSource.volume = volume;

        musicSource.Play();
    }


    static public SoundManager Instance
    {
        get { return _instance;}
        set { _instance = value;}
    }
}
