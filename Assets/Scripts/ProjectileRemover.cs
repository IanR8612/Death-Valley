using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRemover : MonoBehaviour
{
    public GameObject Projectile;
    private float SecondsUntilDeath = 1.5f;
    
    private void Start()
    {
        StartCoroutine(WaitUntilDeath());
    }

    IEnumerator WaitUntilDeath()
    {
        yield return new WaitForSeconds(SecondsUntilDeath);
        Death();
    }

    private void Death()
    {
        Destroy(Projectile);
    }
}
