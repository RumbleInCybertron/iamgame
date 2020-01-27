using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickups : MonoBehaviour
{
    [SerializeField] AudioClip katanaPickupFX;
    [SerializeField] string katana = "Katana";

    // Destroys pickup gameobject when player triggers a collision on it.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Player>() == null) { return; }

        float volume = PlayerPrefs.GetFloat("master_volume");
        AudioSource.PlayClipAtPoint(katanaPickupFX, Camera.main.transform.position, volume);
        Destroy(gameObject);
    }
}
