using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject Projectile;
    public GameObject ProjectileSpawnLocation;
    public float FireRate = 0.1f;
    public float ProjectileSpeed = 3000f;
    public float RandomSpeedMultiplier = 0.1f;
    public float Spread = 0.001f;
    public int ProjectilesPerShot = 1;
    public int Damage = 1;
    private bool canFire = true;

    public void SetVariables(GameObject playerObject)
    {
        PlayerObject = playerObject;
    }

    private void Update()
    {
        if (Input.GetKey("mouse 0") && canFire)
            FireProjectile();
    }

    private void FireProjectile()
    {
        for (int a = 1; a <= ProjectilesPerShot; a += 1)
        {
            float randomRotation = Random.Range(-Spread, Spread);
            float randomSpeedMultiplier = Random.Range(-RandomSpeedMultiplier + 1, RandomSpeedMultiplier + 1);
            GameObject newProjectile = Instantiate(Projectile, ProjectileSpawnLocation.transform.position, PlayerObject.transform.rotation);
            newProjectile.GetComponent<Projectile>().SetDamage(Damage);
            newProjectile.GetComponent<Projectile>().SetTeam(PlayerObject.gameObject.tag);
            newProjectile.transform.Rotate(new Vector3(randomRotation, 0, 0));
            newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * (ProjectileSpeed * randomSpeedMultiplier));
        }
        canFire = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(FireRate);
        canFire = true;
    }
}
