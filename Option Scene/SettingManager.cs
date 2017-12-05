using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour {

    public Toggle fullScreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureDropdown;
    public Slider musicVolumeSlider;
    public Button applyButton;

    public Resolution[] resolutions;
    public GameSettings gameSettings;


    // Use this for initialization
    void OnEnable ()
    {
        gameSettings = new GameSettings();

        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureDropdown.onValueChanged.AddListener(delegate { OnTextureChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApply(); });

        //adds all the different resolution that are possible to the dropdown.
        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();


    }

    public void OnFullScreenToggle()
    {
        gameSettings.fullScreen = Screen.fullScreen = fullScreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textQuality = textureDropdown.value;
        
    }

    public void OnMusicVolumeChange()
    {
        AudioListener.volume = gameSettings.musicVolume = musicVolumeSlider.value;
    }    

    public void OnApply()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
        
        
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

        fullScreenToggle.isOn = gameSettings.fullScreen;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        textureDropdown.value = gameSettings.textQuality;
        musicVolumeSlider.value = gameSettings.musicVolume;


        Screen.fullScreen = gameSettings.fullScreen;
        resolutionDropdown.RefreshShownValue();

    }
}
