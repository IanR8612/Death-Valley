using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject ProjectileObject;
    private int damage;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetTeam(string team)
    {
        ProjectileObject.gameObject.tag = team;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ProjectileObject.gameObject.tag == other.gameObject.tag)
        {
            return;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        else if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(damage);
            Debug.Log("-1 HP");
            DestroyProjectile();
        }
        else
        {
            return;
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
