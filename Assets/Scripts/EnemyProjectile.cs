using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rb;

    private GameObject player;
    private Vector2 target;

    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = (player.transform.position - transform.position).normalized * Speed;
        }
        rb.velocity = new Vector2(target.x, target.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
            Debug.Log(player.GetComponent<Player>().ShowHealth());
        }

        Physics2D.IgnoreLayerCollision(10, 11);
    }
}
