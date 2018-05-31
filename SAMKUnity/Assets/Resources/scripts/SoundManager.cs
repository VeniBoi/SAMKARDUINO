using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;           // For longer effect sounds
    public AudioSource efxSource2;          // For short effect sounds
    public AudioSource backgroundSource;    // Background audio

    public static SoundManager instance = null;

    // Add variability to pitch (slight change only)
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    public bool keepFadingIn;
    public bool keepFadingOut;



    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
            
    }

    public void MuteBackgroundSounds()
    {
        backgroundSource.mute = true;
    }

    public void UnmuteBackgroundSounds()
    {
        backgroundSource.mute = false;
    }


    // For short effect sounds
    public void PlaySingle(AudioClip clip, float volume)
    {
        efxSource2.clip = clip;
        efxSource2.volume = volume;
        efxSource2.Play();
    }

    public void FadeInCaller(AudioClip clip, float speed, float maxVolume)
    {
        instance.StartCoroutine(FadeIn(clip, speed, maxVolume));
    }

    public void FadeOutCaller(AudioClip clip, float speed, float maxVolume)
    {
        instance.StartCoroutine(FadeOut(clip, speed, maxVolume));
    }

    public IEnumerator FadeIn (AudioClip clip, float speed, float maxVolume)
    {
        keepFadingIn = true;
        keepFadingOut = false;

        efxSource.clip = clip;
        efxSource.Play();

        efxSource.volume = 0;
        float audioVolume = efxSource.volume;

        

        while (efxSource.volume < maxVolume && keepFadingIn)
        {
            audioVolume += speed;
            efxSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
      
    }

    public IEnumerator FadeOut(AudioClip clip, float speed, float pauseBeforeFadeOut)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        
        float audioVolume = efxSource.volume;

        yield return new WaitForSeconds(pauseBeforeFadeOut);

        while (efxSource.volume >= speed && keepFadingOut)
        {
            audioVolume -= speed;
            efxSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }

    }


    public void ToggleSoundOnorOff()
    {
        bool toggle = true;

        if (PlayerPrefs.HasKey("SoundToggleValue"))
        {
            if (PlayerPrefs.GetInt("SoundToggleValue") == 1)
                toggle = true;
            else
                toggle = false;
        }

        
        if (toggle)
            AudioListener.volume = 1f;

        else
            AudioListener.volume = 0f;
    }




    /*
    public void PlaySingleForTime(AudioClip clip, float time)
    {
        efxSource.clip = clip;
        efxSource.volume = 0.8f;
        efxSource.Play();

        for (int i = 100; i > 0; i--)
        {
            efxSource.volume -= Time.deltaTime / time;
            Debug.Log(efxSource.volume);
        }


    }
    */


    // Update is called once per frame
    void Update () {
		
	}
}
