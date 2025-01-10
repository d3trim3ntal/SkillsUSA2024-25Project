using System.Collections;
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
    public Button playBttn;

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
        playBttn.onClick.AddListener(() => StartCoroutine(ChangeSceneAfterDelay(1f)));
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

    public IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainBaseLevel1");
    }

    public void Sandbox()
    {
        SceneManager.LoadScene("SandboxMessingAround");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("SOULMM");
    }

    public void TestLevel()
    {
        SceneManager.LoadScene("TestingLevelOne");
    }
}