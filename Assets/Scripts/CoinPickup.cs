using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX; 
    [SerializeField] int scoreMultiplier = 100;
    bool isPicked=false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isPicked)
        {
            isPicked=true;
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddtoScore(scoreMultiplier) ;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
