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
    public float MoveSpeed = 20f;
    public Rigidbody2D rb;
    private Vector2 movement;
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
        MovementInput();
        LookAtMouse();
    }

    private void FixedUpdate()
    {
        MovementPhysics();
    }

    private void MovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void MovementPhysics()
    {
        rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime);
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
