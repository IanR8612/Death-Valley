using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float Speed;

    private Rigidbody rb;

    private GameObject player;

    private Player playerHealth;
    private Vector3 target;

    private int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player>();

        target = (player.transform.position - transform.position).normalized * Speed;
        rb.velocity = new Vector2(target.x, target.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>();
            playerHealth.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
