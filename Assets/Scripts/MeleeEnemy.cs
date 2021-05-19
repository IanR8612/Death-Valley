using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    public float AttackDamage = 1;

    public Vector3 AttackOffset;
    public float AttackRange = 1f;
    public LayerMask AttackMask;

    public AudioSource meleeSFX;

    public void Attack()
    {
        meleeSFX.Play();
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, AttackRange, AttackMask);

        if(colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(AttackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;

        Gizmos.DrawWireSphere(pos, AttackRange);
    }
}
