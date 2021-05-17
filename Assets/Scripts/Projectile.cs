using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool canPenetrate = false;
    private float damage;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public void SetTeam(string team)
    {
        this.gameObject.tag = team;
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (this.gameObject.tag == other.gameObject.tag)
        {
            return;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage((int)damage);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "EnemyProjectile")
        {
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            if (!canPenetrate)
            {
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.tag == "EnemyProjectile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}