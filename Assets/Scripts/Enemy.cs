using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public float Health;
    public float MaxHealth = 5;

    public HealthbarBehavior Healthbar;

    void Start()
    {
        Health = MaxHealth;
    }

    void Update()
    {
        Healthbar.SetHealth(Health, MaxHealth);
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
}