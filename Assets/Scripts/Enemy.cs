using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    /*
     * 1. 
     * a. Dominic Castaneda
     * b. 2339062
     * c. dcastaneda@chapman.edu
     * d. CPSC 245-01
     * e. Final
     * f. This is my own work, and I did not cheat on this assignment.

    2. This script is set to the Enemy prefab. This script sets the enemy health, damage and flipped rotation.
    */
    public Animator animator; //enemy animator

    private float Health; //current health for healthbar purposes
    public float MaxHealth = 5; //max health

    public bool isFlipped = false; //flips enemy

    public HealthbarBehavior Healthbar; //healthbar script

    private Transform player;

    public Transform dropPoint;
    public GameObject HealthPickUp;

    void Start()
    {
        //sets health to maxhealth on spawn
        Health = MaxHealth;
        //sets target to object with tag "player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //updates healthbar parameters
        Healthbar.SetHealth(Health, MaxHealth);
        //updates player Transform to "player" tag in scene
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    //this method is for the enemy taking damage
    public void TakeDamage(float damage)
    {
        Health -= damage;
        //if health is greater than 0, show flinch animation
        if(Health > 0)
        {
            animator.SetTrigger("Hurt");
        }
        //when enemy takes damage, update healthbar
        Healthbar.SetHealth(Health, MaxHealth);
        //if enemy health is 0, die
        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        //play enemy death animation
        animator.SetBool("isDead", true);
        //adds enemy's death to killcount
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddToKillCount();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false; //disable enemy - doesn't seem to work atm
        //destroy enemy after .6 seconds to show death animation
        Destroy(gameObject, 0.6f);

        // Health pack added by Jae
        int dropChance = Random.Range(1, 18);
        if (dropChance == 7)
        {
            Instantiate(HealthPickUp, dropPoint.position, dropPoint.rotation);
        }
        Destroy(gameObject);
    }

    //this method flips the enemy depending on the enemies position relative to the player
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