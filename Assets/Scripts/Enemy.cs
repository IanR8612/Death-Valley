using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public float Health;
    public float MaxHealth = 5;

    public bool isFlipped = false;

    public HealthbarBehavior Healthbar;

    private Transform player;

    void Start()
    {
        Health = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Healthbar.SetHealth(Health, MaxHealth);
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health > 0)
        {
            animator.SetTrigger("Hurt");
        }
        Healthbar.SetHealth(Health, MaxHealth);
        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddToKillCount();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false; //disable enemy
        Destroy(gameObject, 1.0f);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (player != null)
        {
            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }
    }
}