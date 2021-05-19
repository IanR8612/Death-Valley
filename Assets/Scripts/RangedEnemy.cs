using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    //public float StoppingDistance;
    //public float RetreatDistance;
    public float StartTimeBtwShots;

    private float timeBtwShots;
    public GameObject EnemyProjectile;

    private Animator anim;

    private Transform Player;

    public AudioSource rangedSFX;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timeBtwShots = StartTimeBtwShots;
    }

    private void Update()
    {
        Shoot();
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        FlipRanged();
    }

    public void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            rangedSFX.Play();
            Instantiate(EnemyProjectile, transform.position, Quaternion.identity);
            timeBtwShots = StartTimeBtwShots;
            animator.SetTrigger("Attack");
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            animator.ResetTrigger("Attack");
        }
    }

    public void FlipRanged()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (Player != null)
        {
            if (transform.position.x > Player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < Player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }
    }

}
