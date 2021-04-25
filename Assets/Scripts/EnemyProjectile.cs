using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    public float Speed;

    public Rigidbody rb;

    public Player player;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player>();


        target = (player.transform.position - transform.position).normalized * Speed;
        rb.velocity = new Vector2(target.x, target.y);
    }
}
