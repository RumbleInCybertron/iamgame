using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] static int playerLives = 3;
    [SerializeField] static int score = 0;

    [SerializeField] Text scoreText;
    [SerializeField] Image[] hearts;
    [SerializeField] Transform optionsMenu;

    #region Game Settings
    private static string volumeKey = "master_volume";
    private static string graphicsKey = "graphics_preset";
    private static string fullscreenKey = "is_fullscreen";

    public static void SetVolume(float value)
    {
        PlayerPrefs.SetFloat(volumeKey, value);
        PlayerPrefs.Save();
    }

    public static float GetVolume()
    {
        if (!PlayerPrefs.HasKey(volumeKey))
        {
            SetVolume(1);
            return 1;
        }
        return PlayerPrefs.GetFloat(volumeKey);
    }

    public static void SetGraphicsPreset(int value)
    {
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt(volumeKey, value);
        PlayerPrefs.Save();
    }

    public static int GetGraphicsPreset()
    {
        if (!PlayerPrefs.HasKey(graphicsKey))
        {
            SetGraphicsPreset(2);
            return 2;
        }
        return PlayerPrefs.GetInt(graphicsKey);
    }

    public static void SetFullscreen(bool value)
    {
        Screen.fullScreenMode = value ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        int intValue = value ? 1 : 0;
        PlayerPrefs.SetInt(fullscreenKey, intValue);
        PlayerPrefs.Save();
    }

    public static bool GetFullscreen()
    {
        if (!PlayerPrefs.HasKey(fullscreenKey))
        {
            SetFullscreen(true);
            return true;
        }
        return PlayerPrefs.GetInt(fullscreenKey) == 1 ? true : false;
    }
    #endregion

    #region Player Data
    public static void ResetSession()
    {
        playerLives = 3;
        score = 0;
    }

    public static void LoadGame(PlayerData data)
    {
        playerLives = data.lives;
        score = data.score;
        SceneManager.LoadScene(data.levelIndex);
    }
    #endregion

    public static int getPlayerLives() { return playerLives; }
    public static int getPlayerScore() { return score; }

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        scoreText.text = score.ToString();
        SetLives();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
    }

    private void SetLives()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < playerLives)
                hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }

    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        SetLives();
    }

    private void ResetGameSession()
    {
        ResetSession();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void ToggleMenu()
    {
        optionsMenu.gameObject.SetActive(!optionsMenu.gameObject.activeSelf);
    }

}
