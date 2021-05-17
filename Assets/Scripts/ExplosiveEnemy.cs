using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy
{

    public float AttackDamage = 1;

    public Vector3 AttackOffset;
    public float GizmoRange = 1f;
    public LayerMask AttackMask;

    public void Explode()
    {
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, GizmoRange, AttackMask);

        if(colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(AttackDamage);
            Destroy(gameObject, 0.2f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;

        Gizmos.DrawWireSphere(pos, GizmoRange);
    }
}
