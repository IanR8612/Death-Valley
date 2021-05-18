﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAnimation : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Enemy enemy;

    public float AttackRange = 5f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();
        if (player != null)
        {
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
