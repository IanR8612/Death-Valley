﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public Transform enemyRange;
        public int count;
        public float rate;
    }

    public int waveCounter = 1;

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public Text gunSpawn;
    public Text spawnCheck;
    public Text waveCount;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Error. No spawn points referenced.");
        }        

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {            
            if (!EnemyIsAlive())
            {
                //Debug.Log("All enemies dead");
                //gunSpawn.text = "spawn";
                if (gunSpawn.text == "spawn")
                {
                    Invoke("PickUpWeapon", 1);
                    //PickUpWeapon();
                }
            }
            //Debug.Log("Waiting");
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (gunSpawn.text == "despawn") //if wavecounter = wavecounter += 1
            {
                if (state != SpawnState.SPAWNING)
                {
                    waveCounter += 1;
                    StartCoroutine(SpawnWave(waves[nextWave]));                    
                    waveCount.text = "Wave: " + waveCounter.ToString();
                }
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void PickUpWeapon()
    {               
        waveCountdown = timeBetweenWaves;
        if (GameObject.FindGameObjectsWithTag("Weapon").Length == 1)
        {
            WaveCompleted();
        }
        //else
        //{
        //    PickUpWeapon();
        //}
        //add button to skip weapon pickup
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        //waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            //add if statement to check if there are any weapons in the game, if not - send to next wave
            //also check if next round button has been clicked?
            nextWave = 0;
            //ADD ON STAT MULTIPLIER, GAME FINISHED SCREEN
            //Debug.Log("All Waves Complete! Looping...");  
            EnemyCount(waves[nextWave]);
            EnemyRate(waves[nextWave]);
            spawnCheck.text = "1";            
        }
        else
        {
            nextWave++;
        }
    }

    void EnemyRate(Wave _wave)
    {
        _wave.rate += 0.1f;
    }

    void EnemyCount(Wave _wave)
    {
        _wave.count += 2;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {            
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gunSpawn.text = "spawn";
                //Debug.Log("Enemies dead, returning false");
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;        
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);         
            SpawnEnemy(_wave.enemyRange);
            yield return new WaitForSeconds(1f / _wave.rate);
            //_wave.delay in () for a delay
        }
        state = SpawnState.WAITING;
        //Debug.Log("Waiting");

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
