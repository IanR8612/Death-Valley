using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy
{
    /*
     * 1. 
     * a. Dominic Castaneda
     * b. 2339062
     * c. dcastaneda@chapman.edu
     * d. CPSC 245-01
     * e. Final
     * f. This is my own work, and I did not cheat on this assignment.

    2. This script is set to the ExplosiveEnemy prefab. This script sets the enemy attack mask(player), damage and gizmo range.
    */
    public float AttackDamage = 1; //enemy attack damage
    public float GizmoRange = 6f; //gizmo range (attack range)

    public Vector3 AttackOffset; //positioning of the attack gizmo
    public LayerMask AttackMask; //enemies attack on this layer

    public AudioSource explosiveSFX;

    //public GameObject lootDrop;

    void Update()
    {
        //calls Enemy.cs method to flip enemy sprite
        LookAtPlayer();
    }

    //this method causes the ExplosiveEnemy to damage the player
    //is triggered by an event in the ExplosiveEnemy_Attack animation
    public void Explode()
    {
        explosiveSFX.Play();
        //position of gizmo (attack distance)
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;

        //checks if collider falls in this circular area on specified layermask
        Collider2D colInfo = Physics2D.OverlapCircle(pos, GizmoRange, AttackMask);

        //when collision occurs, damage player
        if(colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(AttackDamage);
            // animator.SetTrigger("Explode");
            //Instantiate(lootDrop, transform.position, Quaternion.identity);
            //if (drop) Instantiate(HealthPickUp, dropPoint.position, dropPoint.rotation);
        }


        //explosive enemy should die after explosion
        Destroy(gameObject, 0.2f);

    }

    //draws gizmo to show the range of the explosion
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;

        Gizmos.DrawWireSphere(pos, GizmoRange);
    }
}
