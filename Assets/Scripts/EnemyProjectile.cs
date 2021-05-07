using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rb;

    private GameObject player;

    private Player playerHealth;
    private Vector2 target;

    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player>();

        target = (player.transform.position - transform.position).normalized * Speed;
        rb.velocity = new Vector2(target.x, target.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>();
            playerHealth.TakeDamage(damage);
            Destroy(this.gameObject);
        }       
    }
}
