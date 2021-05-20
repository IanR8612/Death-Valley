using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10;
    public float currentHealth = 10;
    public Player playerToDie;
    public ScoreManager highScoreController;

    public AudioSource loseHealthSFX;

    // public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        loseHealthSFX.Play();
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            highScoreController.UpdateHighScore();
            playerToDie.Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;

        }
    }

}
