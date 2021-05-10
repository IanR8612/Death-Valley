using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject EnemyObject;

    public float Speed;
    public float StoppingDistance;
    public float RetreatDistance;
    public float StartTimeBtwShots;

    private float timeBtwShots;

    private Transform player;
    public GameObject EnemyProjectile;
    private Rigidbody2D rb;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        timeBtwShots = StartTimeBtwShots;
    }

    private void Update()
    {

        if (player != null)
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


            if (timeBtwShots <= 0)
            {
                Instantiate(EnemyProjectile, transform.position, Quaternion.identity);
                timeBtwShots = StartTimeBtwShots;
                anim.SetBool("Attack", true);
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
                anim.SetBool("Attack", false);
            }
        }
    }
}
