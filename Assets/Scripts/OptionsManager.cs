using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown qualityDropDown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    private Resolution[] resolutions;

    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));


        ResolutionSetting();
        gameManager.audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;
        qualityDropDown.value = PlayerPrefs.GetInt("Quality");
    }


    public void SetVolume()
    {
        gameManager.audioMixer.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityDropDown.value);
        PlayerPrefs.SetInt("Quality", qualityDropDown.value);
        PlayerPrefs.Save();
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    void ResolutionSetting()
    {
        resolutions = Screen.resolutions.Distinct().ToArray();
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResIndex;
        resolutionDropDown.RefreshShownValue();
    }


    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
