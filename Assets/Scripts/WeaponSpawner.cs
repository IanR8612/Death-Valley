using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    float searchCountdown = 1f;

    [System.Serializable]
    public class Weapons
    {
        public string name;
        public Transform weapon;
    }

    public Transform[] WeakWeapons;
    public Transform[] MediumWeapons;
    public Transform[] StrongWeapons;

    //public Weapons[] WeakWeapons;
    //public Weapons[] MediumWeapons;
    //public Weapons[] StrongWeapons;

    private Transform chosenWeapon;

    public int roundCounter = 0;
    private int gunCount = 1;

    //private float searchTime = 4f;

    public Text gunSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (roundCounter != 0)
        {
            roundCounter = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {     
        if (!EnemyIsAlive())
        {            
            if (gunSpawn.text == "spawn")
            {
                gunCount = 1;
                //if (searchTime == 0f)
                //{
                //    WeaponCheck();
                //}
                WeaponCheck();
            }            
        }
        else
        {
            //searchTime -= Time.deltaTime;
            return;
        }
    }

    void WeaponCheck()
    {
        if (roundCounter <= 5)
        {
            chosenWeapon = WeakWeapons[Random.Range(0, WeakWeapons.Length)];
            if (GameObject.FindGameObjectsWithTag("Weapon").Length == gunCount)
            {
                //Invoke("SpawnWeakWeapon(chosenWeapon)", 1);
                SpawnWeakWeapon(chosenWeapon);
                gunCount = 2;
            }                
        }

        if (roundCounter <= 10)
        {
            chosenWeapon = MediumWeapons[Random.Range(0, MediumWeapons.Length)];
            if (GameObject.FindGameObjectsWithTag("Weapon").Length == gunCount)
            {
                SpawnMedWeapon(chosenWeapon);
                gunCount = 2;
            }
        }

        if (roundCounter > 10)
        {
            chosenWeapon = StrongWeapons[Random.Range(0, StrongWeapons.Length)];
            if (GameObject.FindGameObjectsWithTag("Weapon").Length == gunCount)
            {
                SpawnStrongWeapon(chosenWeapon);
                gunCount = 2;
            }
        }
    }

    public void DestroyWeapon()
    {
        //Debug.Log("called");
        Destroy(chosenWeapon.gameObject);
    }

    void SpawnWeakWeapon(Transform _weapon)
    {
        //Transform _wpType = WeakWeapons[Random.Range(0, WeakWeapons.Length)];        
        Instantiate(_weapon, spawnPoint.position, spawnPoint.rotation);
        gunSpawn.text = "despawn";
        roundCounter += 1;
        //searchTime = 4f;
    }
    void SpawnMedWeapon(Transform _weapon)
    {

    }
    void SpawnStrongWeapon(Transform _weapon)
    {

    }

    bool EnemyIsAlive()
    {        
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
}
