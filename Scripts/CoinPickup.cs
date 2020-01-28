using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int pointsPerCoinPickup = 100;
    [SerializeField] string gameStarterTag = "GameStarter";
    [SerializeField] string continueGameTag = "ContinueGame";
    [SerializeField] string mainMenuCoinsString = "Main Menu Coins";

    [SerializeField] Transform continueGameText = null;

    // Destroy coin when player triggers a collision on coin.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.tag);
        if (gameObject.CompareTag(gameStarterTag))
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerData.EraseSaveFile();
            GameSession.ResetSession();
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if (gameObject.CompareTag(continueGameTag))
        {
            Debug.Log("Player wants to continue game.");
            bool hasLoaded = PlayerData.LoadPlayerData();
            if (!hasLoaded)
                continueGameText.GetComponent<TextMeshPro>().color = new Color(.5f, .5f, .5f);
        }
        else
        {
            float volume = GameSession.GetVolume();
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position, volume);
            if (gameObject.transform.parent.name != mainMenuCoinsString)
                FindObjectOfType<GameSession>().AddToScore(pointsPerCoinPickup);
            Destroy(gameObject);
        }
    }

}
