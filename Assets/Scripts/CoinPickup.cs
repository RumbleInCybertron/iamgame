using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int pointsPerCoinPickup = 100;
    [SerializeField] string gameStarterTag = "GameStarter";

    // Destroy coin when player triggers a collision on coin.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            FindObjectOfType<GameSession>().AddToScore(pointsPerCoinPickup);
        }
        catch (System.Exception)
        {
            Debug.Log("Probably in the main menu, thus - no score");
        }

        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
        if (gameObject.tag == gameStarterTag)
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        Destroy(gameObject);
    }

}
