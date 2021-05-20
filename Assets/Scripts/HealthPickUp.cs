using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    PlayerHealth playerHealth;

    public float healthBonus = 15f;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "Player(Clone)")
        { 
            if(playerHealth.currentHealth < playerHealth.maxHealth)
            {
                Destroy(gameObject);
                playerHealth.currentHealth = playerHealth.currentHealth + healthBonus;
            }
        }
    }
}
