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
    private bool canPickUp = false;
    private bool canFire = true;
    private GameObject otherObject;


    public void SetVariables(GameObject playerObject)
    {
        PlayerObject = playerObject;
    }

    private void Update()
    {
        if (Input.GetKey("mouse 0") && canFire && PlayerObject != null)
            FireProjectile();
        if (Input.GetKeyDown("e") && PlayerObject == null && canPickUp)
        {
            SetPlayerObject();
        }
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

    private void SetPlayerObject()
    {
        if (otherObject.CompareTag("Player"))
        {
            PlayerObject = otherObject;
            this.gameObject.transform.SetParent(PlayerObject.transform);
            this.gameObject.transform.position.Set(0f, 0f, 0f);
            PlayerObject.GetComponent<Player>().PickUpNewWeapon(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        canPickUp = true;
        otherObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        canPickUp = false;
        otherObject = null;
        }
    }
}
