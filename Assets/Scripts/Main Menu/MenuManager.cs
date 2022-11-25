using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // TODO Slider
    public Toggle musicToggle;
    public Slider musicSlider;

    // TODO berganti scene
    public void PlayScene(){
        SceneManager.LoadScene("Platformer 2");
    }

    private void Awake()
    {
        // TODO Mute
        if (SoundManager.Instance.musicSource.mute == true)
        {
            musicToggle.isOn = false;
            Debug.Log("Status Music Mute:" + SoundManager.Instance.musicSource.mute);
        }
        else
        {
            musicToggle.isOn = true;
            Debug.Log("Status Music Mute :" + SoundManager.Instance.musicSource.mute);
        }
    }

    void Start()
    {
        // TODO Load Volume
        musicSlider.value = SoundManager.Instance.musicSource.volume;
        SoundManager.Instance.MusicLoadVolume();
        Debug.Log("Volume Music : " + musicSlider.value);
    }

    // TODO slider volume
    public void MusicSliderVolume()
    {
        SoundManager.Instance.musicSource.volume = musicSlider.value;
        Debug.Log("Volume Music : " + musicSlider.value);
    }

    // TODO mute
    public void MusicSetMute()
    {
        if (musicToggle.isOn == true)
        {
            SoundManager.Instance.musicSource.mute = false;
            Debug.Log("Status Music Mute :" + SoundManager.Instance.musicSource.mute);
        }
        else
        {
            SoundManager.Instance.musicSource.mute = true;
            Debug.Log("Status Music Mute :" + SoundManager.Instance.musicSource.mute);
        }
    }

    // TODO Graphic Quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // TODO Open link discord
    public void OpenDiscord()
    {
        Application.OpenURL("https://discord.gg/56zDbKzyV3");
    }

    // TODO keluar game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Keluar Aplikasi");
    }
}
