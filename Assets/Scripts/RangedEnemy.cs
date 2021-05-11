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

    private Transform player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timeBtwShots = StartTimeBtwShots;
    }

    private void Update()
    {
        /*if (Vector2.Distance(transform.position, player.position) > StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) < StoppingDistance && Vector2.Distance(transform.position, player.position) > RetreatDistance)
        {
            transform.position = this.transform.position;
        }

        else if (Vector2.Distance(transform.position, player.position) < RetreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -Speed * Time.deltaTime);
        }*/
        Shoot();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        FlipRanged();
    }

    public void Shoot()
    {
        if (timeBtwShots <= 0)
        {
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
