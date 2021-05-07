using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Camera Camera;
    public GameObject WeaponSlotOnePrefab;
    public GameObject WeaponParent;
    private GameObject weaponSlotOne;
    private float moveSpeed = 0.1f;
    private float health = 5;
    private int killCount = 0;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health == 0)
            Die();
    }

    public void AddToKillCount()
    {
        killCount += 1;
    }

    public void PickUpNewWeapon(GameObject weapon)
    {
        Destroy(weaponSlotOne.gameObject);
        weaponSlotOne = weapon;
        weaponSlotOne.transform.SetParent(WeaponParent.transform);
        weaponSlotOne.transform.SetPositionAndRotation(WeaponParent.transform.position, WeaponParent.transform.rotation);
        weaponSlotOne.transform.Rotate(0, -90, 0);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void Start()
    {
        InitializeWeapons();
    }

    private void Update()
    {
        HandleMovement();
        LookAtMouse();
    }

    private void HandleMovement()
    {
        Vector3 newPosition = this.gameObject.transform.position;
        
        if (Input.GetKey("w"))
        {
            newPosition.y += moveSpeed;
        }
        if (Input.GetKey("s"))
        {
            newPosition.y -= moveSpeed;
        }
        if (Input.GetKey("a"))
        {
            newPosition.x -= moveSpeed;
        }
        if (Input.GetKey("d"))
        {
            newPosition.x += moveSpeed;
        }

        this.gameObject.transform.position = newPosition;
    }

    private void LookAtMouse()
    {
        Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        WeaponParent.transform.LookAt(mousePosition);
    }

    private void InitializeWeapons()
    {
        weaponSlotOne = InstantiateWeaponPrefab(WeaponSlotOnePrefab);
    }

    private GameObject InstantiateWeaponPrefab(GameObject prefab)
    {
        GameObject newWeapon = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, this.gameObject.transform);
        newWeapon.transform.SetParent(WeaponParent.transform);
        newWeapon.transform.Rotate(new Vector3(0, 0, -90));
        Weapon weaponScript = (Weapon)newWeapon.GetComponent(typeof(Weapon));
        weaponScript.SetVariables(this.gameObject);
        return newWeapon;
    }
}
