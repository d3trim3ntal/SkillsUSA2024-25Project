/*using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine;

public class UISetsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public Light sceneLight;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(delegate { onVolumeChanged(); });

        brightnessSlider.value = sceneLight.intensity;
        FindAnyObjectByType<AudioManager>().Play("MainMenu");

        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    void onVolumeChanged()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void VolumeChange(float value)
    {
        float localValue = value;
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void BrightChange(float value)
    {
        float localValue = value;
    }

    public void AdjustBrightness(float newBrightness)
    {
        sceneLight.intensity = newBrightness;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void PlayAudio()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
    }
}
*/