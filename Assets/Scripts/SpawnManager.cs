using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpawnManager : MonoBehaviour
{
    public int waveAmount = 5;
    public int waveNumber = 0;
    public float waveDelayTimer = 3f;
    public float waveCoolDown = 20f;
    public int maxWaves = 500;
    public GameObject enemy;
    public int[] enemyCount;
    public GameObject[] SpawnPoints;
    public GameObject[] pathPoints;

    private void Awake()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToArray<GameObject>();
    }
    public bool IsLastWave()
    {
        if (maxWaves == waveNumber)
            return true;
        else
            return false;
    }
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