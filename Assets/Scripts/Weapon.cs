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
    public float Damage = 1;
    public Sprite noHandSprite;
    public Sprite handSprite;
    public SpriteRenderer currentSprite;
    public bool doesSpriteNeedtoSwitch = true;
    private bool canPickUp = false;
    private bool canFire = true;
    private GameObject otherObject;

    public AudioSource projectileSound;
    public AudioSource pickupSound;


    public void SetVariables(GameObject playerObject)
    {
        PlayerObject = playerObject;
        ChangeSprite();
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
            projectileSound.Play();
            float randomRotation = Random.Range(-Spread, Spread);
            float randomSpeedMultiplier = Random.Range(-RandomSpeedMultiplier + 1, RandomSpeedMultiplier + 1);
            GameObject newProjectile = Instantiate(Projectile, ProjectileSpawnLocation.transform.position, this.gameObject.transform.rotation);
            newProjectile.GetComponent<Projectile>().SetDamage(Damage);
            newProjectile.GetComponent<Projectile>().SetTeam(PlayerObject.gameObject.tag);
            newProjectile.transform.Rotate(new Vector3(0, 0, randomRotation - 90));
            newProjectile.GetComponent<Rigidbody2D>().AddForce(newProjectile.transform.up * (ProjectileSpeed * randomSpeedMultiplier));
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
            pickupSound.Play();
            PlayerObject = otherObject;
            this.gameObject.transform.SetParent(PlayerObject.transform);
            PlayerObject.GetComponent<Player>().PickUpNewWeapon(this.gameObject);
            ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        if (doesSpriteNeedtoSwitch)
            currentSprite.sprite = handSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        canPickUp = true;
        otherObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        canPickUp = false;
        otherObject = null;
        }
    }
}
