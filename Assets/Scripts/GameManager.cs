using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int instanceID = 0;
    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public int genderID;
    public AudioMixer audioMixer;
    public int quality;
    public bool fullscreen;


    public void Save()
    {
        SaveSystem.SaveStats(this);
    }
    public void Load()
    {
        PlayerData data = SaveSystem.LoadStats();

        genderID = data.genderID;
    }



    public void LoadOptionsSettings()
    {
        if(PlayerPrefs.HasKey("Volume"))
            audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        else
            audioMixer.SetFloat("Volume", -40f);

        if (PlayerPrefs.HasKey("Quality"))
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        else
            QualitySettings.SetQualityLevel(2);

        if (PlayerPrefs.HasKey("Fullscreen"))
            Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;
        else
            Screen.fullScreen = true;
    }
}
