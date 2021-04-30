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
    private Rigidbody rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody>();
        timeBtwShots = StartTimeBtwShots;
    }

    private void Update()
    {
        transform.LookAt(player);

        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > StoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
            }

            else if (Vector3.Distance(transform.position, player.position) < StoppingDistance && Vector3.Distance(transform.position, player.position) > RetreatDistance)
            {
                transform.position = this.transform.position;
            }

            else if (Vector3.Distance(transform.position, player.position) < RetreatDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -Speed * Time.deltaTime);
            }


            if (timeBtwShots <= 0)
            {
                Instantiate(EnemyProjectile, transform.position, Quaternion.identity);
                timeBtwShots = StartTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
