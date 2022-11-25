using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicSource;
    public Toggle musicToggle;
    public Slider musicSlider;

    // * agar audio bisa di putar di berbagai scene
    public static SoundManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // * untuk mengatur volume music dengan slidder
    public void MusicSaveVolume()
    {
        Instance.musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicValue", musicSlider.value);
        MusicLoadVolume();
        Debug.Log("Set Volume" + musicSlider.value);
    }

    // * untuk meload volume music
    public void MusicLoadVolume()
    {
        float musicVolumeValue = PlayerPrefs.GetFloat("musicValue");
        musicSlider.value = musicVolumeValue;
        Debug.Log("VOLUME SET LOAD VALUE : " + musicVolumeValue);
    }

    // * mute music
    public void MuteSound()
    {
        if (musicToggle.isOn == true)
        {
            musicSource.mute = false;
        }
        else
        {
            musicSource.mute = true;
        }
    }
}
