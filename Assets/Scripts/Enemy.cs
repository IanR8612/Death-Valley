using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 5;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddToKillCount();
    }
}