using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rb;

    private GameObject player;
    private Vector2 target;

    private Animator anim;

    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = (player.transform.position - transform.position).normalized * Speed;
        }
        else
        {
            Destroy(this.gameObject);
        }
        rb.velocity = new Vector2(target.x, target.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            anim.SetTrigger("Explode");
            Destroy(this.gameObject, 0.45f);
            //Debug.Log(player.GetComponent<Player>().ShowHealth());
        }

        Physics2D.IgnoreLayerCollision(10, 11);
    }
}
