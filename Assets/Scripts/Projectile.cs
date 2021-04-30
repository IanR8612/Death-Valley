using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetTeam(string team)
    {
        this.gameObject.tag = team;
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.gameObject.tag == other.gameObject.tag)
        {
            return;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
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
    }


}