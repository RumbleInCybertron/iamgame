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
        float volume = GameSession.GetVolume();
        int graphicsPreset = GameSession.GetGraphicsPreset();
        bool isFullscreen = GameSession.GetFullscreen();

        slider.GetComponent<Slider>().value = volume;
        graphicsDropDown.GetComponent<Dropdown>().value = graphicsPreset;
        QualitySettings.SetQualityLevel(graphicsPreset);
        fullscreenToggle.GetComponent<Toggle>().isOn = isFullscreen;
    }

    public void SetVolume(float value)
    {
        GameSession.SetVolume(value);
    }

    public void SetGraphics(int value)
    {
        GameSession.SetGraphicsPreset(value);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        GameSession.SetFullscreen(isFullscreen);
    }

    public void GoBack()
    {
        gameObject.SetActive(false);
        PlayerPrefs.Save();
    }
}