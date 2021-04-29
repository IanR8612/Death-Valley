using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float Speed;

    private Rigidbody rb;

    private Player player;
    private Vector3 target;

    private int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<Player>();

        target = (player.transform.position - transform.position).normalized * Speed;
        rb.velocity = new Vector2(target.x, target.y);
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.gameObject.tag == other.gameObject.tag)
        {
            return;
        }
        else if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}
