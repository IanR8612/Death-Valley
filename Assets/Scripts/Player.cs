using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Camera Camera;
    public GameObject PlayerObject;
    public GameObject WeaponSlotOnePrefab;
    public GameObject WeaponSlotTwoPrefab;
    private GameObject weaponSlotOne;
    private GameObject weaponSlotTwo;
    private float moveSpeed = 0.1f;
    private int health = 5;
    private int killCount = 0;

    // to-do: remove the second weapon slot

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
            Die();
    }

    public void AddToKillCount()
    {
        killCount += 1;
    }

    private void Die()
    {
        Destroy(PlayerObject);
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
        Vector3 newPosition = PlayerObject.transform.position;
        
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
        if (Input.GetKeyDown("space"))
        {
            SwitchActiveWeapons();
        }

        PlayerObject.transform.position = newPosition;
    }

    private void LookAtMouse()
    {
        Vector3 mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        PlayerObject.transform.LookAt(mousePosition);
    }

    private void InitializeWeapons()
    {
        weaponSlotOne = InstantiateWeaponPrefab(WeaponSlotOnePrefab);
        weaponSlotTwo = InstantiateWeaponPrefab(WeaponSlotTwoPrefab);
        weaponSlotTwo.SetActive(false);
    }

    private GameObject InstantiateWeaponPrefab(GameObject prefab)
    {
        GameObject newWeapon = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, PlayerObject.transform);
        newWeapon.transform.Rotate(new Vector3(0, 0, -90));
        Weapon weaponScript = (Weapon)newWeapon.GetComponent(typeof(Weapon));
        weaponScript.SetVariables(PlayerObject);
        return newWeapon;
    }

    private void SwitchActiveWeapons()
    {
        if (weaponSlotOne.activeSelf == true)
        {
            weaponSlotOne.SetActive(false);
            weaponSlotTwo.SetActive(true);
        }
        else
        {
            weaponSlotOne.SetActive(true);
            weaponSlotTwo.SetActive(false);
        }
    }
}
