using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
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
        Healthbar.SetHealth(Health, MaxHealth);
        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddToKillCount();
    }
}