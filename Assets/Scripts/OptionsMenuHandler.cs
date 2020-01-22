using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuHandler : MonoBehaviour
{
    [SerializeField] Transform settingsMenu;

    private void Awake()
    {
        settingsMenu.gameObject.SetActive(false);
    }

    public void ToggleSettingsMenu()
    {
        Debug.Log(!settingsMenu.gameObject.activeSelf);
        settingsMenu.gameObject.SetActive(!settingsMenu.gameObject.activeSelf);
        gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        Debug.Log("Saving Game...");
        PlayerData.SavePlayerData(SceneManager.GetActiveScene().buildIndex, GameSession.getPlayerLives(), GameSession.getPlayerScore());
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        Debug.Log("Quitting game.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}