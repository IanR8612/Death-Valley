using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAnimation : StateMachineBehaviour
{
    /*
     * 1. 
     * a. Dominic Castaneda
     * b. 2339062
     * c. dcastaneda@chapman.edu
     * d. CPSC 245-01
     * e. Final
     * f. This is my own work, and I did not cheat on this assignment.

    2. This script is set to the ExplosiveEnemy's animator in the ExplosiveEnemy_Walk State and makes the enemy explode
    */
    Transform player; //player location
    Rigidbody2D rb; //animator rigidbody

    public float AttackRange = 5f; //attackrange

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //sets properties
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        rb = animator.GetComponent<Rigidbody2D>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            //when it player is in range, play explosion animation
            if (Vector3.Distance(player.position, rb.position) <= AttackRange)
            {
                animator.SetTrigger("Explode");
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
