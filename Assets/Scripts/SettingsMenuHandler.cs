using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuHandler : MonoBehaviour
{
    [SerializeField] GameObject slider;
    [SerializeField] GameObject graphicsDropDown;
    [SerializeField] GameObject fullscreenToggle;

    private void Awake()
    {
        float volume = PlayerPrefs.GetFloat("master_volume");
        int graphicsPreset = PlayerPrefs.GetInt("graphics_preset");
        bool isFullscreen = PlayerPrefs.GetInt("is_fullscreen") == 1 ? true : false;

        slider.GetComponent<Slider>().value = volume;
        graphicsDropDown.GetComponent<Dropdown>().value = graphicsPreset;
        QualitySettings.SetQualityLevel(graphicsPreset);
        fullscreenToggle.GetComponent<Toggle>().isOn = isFullscreen;
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("master_volume", value);
        PlayerPrefs.Save();
    }

    public void SetGraphics(int value)
    {
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt("graphics_preset", value);
        PlayerPrefs.Save();
    }

    public void SetFullScreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt("is_fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
        Screen.fullScreen = isFullscreen;
    }

    public void GoBack()
    {
        gameObject.SetActive(false);
        PlayerPrefs.Save();
    }
}