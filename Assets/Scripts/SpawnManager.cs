using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpawnManager : MonoBehaviour
{
    /// <summary>
    /// Spawn manager class;
    /// </summary>
    public int waveAmount = 5;
    public int waveNumber = 0;
    public float waveDelayTimer = 3f;
    public float waveCoolDown = 20f;
    public int maxWaves = 500;
    public GameObject enemy;
    public int[] enemyCount;
    public GameObject[] spawnPoints;
    public GameObject[] pathPoints;

    /// <summary>
    /// Fills the array o GameObjects by Spawn Points objects;
    /// </summary>
    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToArray<GameObject>();
    }
    /// <summary>
    /// Checks if current wave of enemies is the last one for this level;
    /// </summary>
    /// <returns>bool</returns>
    public bool IsLastWave()
    {
        if (maxWaves == waveNumber)
            return true;
        else
            return false;
    }
    /// <summary>
    /// Updates the delay time for spawning a new wave of enemies;
    /// Instantietes new enemies each wave untill reaches wave limit for this level;
    /// </summary>
    private void Update()
    {
        waveDelayTimer -= Time.deltaTime;

        if (waveDelayTimer <= 0 && waveNumber < maxWaves)
        {
            waveNumber++;
            waveDelayTimer = waveCoolDown;
            for (int i = 0; i < waveAmount; i++)
            {
                Instantiate(enemy, new Vector3(transform.position.x + i * -0.01f, transform.position.y, 0f), Quaternion.identity);
            }
        }
    }
}