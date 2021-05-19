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

    private int spawn; //0, no spawn   1, spawn

    //private float searchTime = 4f;

    public Text gunSpawn;
    public Text spawnCheck;

    public AudioSource spawnSFX;

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
                gunSpawn.text = "despawn";
                gunCount = 1;
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
                if (spawnCheck.text == "1" && spawn == 1)
                {
                    SpawnWeakWeapon(chosenWeapon);
                    gunCount = 2;
                }
            }                
        }

        else if (roundCounter <= 10 && roundCounter > 5)
        {
            chosenWeapon = MediumWeapons[Random.Range(0, MediumWeapons.Length)];
            if (GameObject.FindGameObjectsWithTag("Weapon").Length == gunCount)
            {
                if (spawnCheck.text == "1" && spawn == 1)
                {
                    SpawnMedWeapon(chosenWeapon);
                    gunCount = 2;
                }
            }
        }

        else if (roundCounter > 10)
        {
            chosenWeapon = StrongWeapons[Random.Range(0, StrongWeapons.Length)];
            if (GameObject.FindGameObjectsWithTag("Weapon").Length == gunCount)
            {
                if (spawnCheck.text == "1" && spawn == 1)
                {
                    SpawnStrongWeapon(chosenWeapon);
                    gunCount = 2;
                }
            }
        }
    }

    void SpawnWeakWeapon(Transform _weapon)
    {
        if (spawn == 1)
        {
            spawn = 0;
            //Transform _wpType = WeakWeapons[Random.Range(0, WeakWeapons.Length)];
            spawnSFX.Play();
            Instantiate(_weapon, spawnPoint.position, spawnPoint.rotation);
            //gunSpawn.text = "despawn";
            roundCounter += 1;
            //searchTime = 4f;
            spawnCheck.text = "0";
        }        
    }
    void SpawnMedWeapon(Transform _weapon)
    {
        if (spawn == 1)
        {
            spawn = 0;
            spawnSFX.Play();
            Instantiate(_weapon, spawnPoint.position, spawnPoint.rotation);
            //gunSpawn.text = "despawn";
            roundCounter += 1;
            spawnCheck.text = "0";
        }
    }
    void SpawnStrongWeapon(Transform _weapon)
    {
        if (spawn == 1)
        {
            spawn = 0;
            spawnSFX.Play();
            Instantiate(_weapon, spawnPoint.position, spawnPoint.rotation);
            //gunSpawn.text = "despawn";
            roundCounter += 1;
            spawnCheck.text = "0";
        }
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
            else
            {
                spawn = 1;
            }
        }        
        return true;
    }
}
