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
        // TODO: Display game over screen
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void ToggleMenu()
    {
        optionsMenu.gameObject.SetActive(!optionsMenu.gameObject.activeSelf);
    }

}
