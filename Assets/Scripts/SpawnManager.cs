using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SpawnModes { Continuous, WaveCleared }

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

    private void Update()
    {
        if (waveDelayTimer > 0)
        {
            waveDelayTimer -= Time.deltaTime;
        }
        else
        {
            waveDelayTimer = 0;
        }

        if (waveDelayTimer <= 0)
        {
            if (SpawnPoints != null && waveNumber < maxWaves)
            {
                Debug.Log(SpawnPoints.Length);
                foreach (GameObject spawnpoint in SpawnPoints)
                {
                    if (waveNumber < maxWaves)
                    {
                        Instantiate(enemy, new Vector3(transform.position.x + 1 * 0.3f, transform.position.y, 0.0f), Quaternion.identity);
                        if (waveDelayTimer > 5)
                        {
                            waveDelayTimer -= 0.1f;
                            waveDelayTimer = waveCoolDown;
                        }
                        else
                        {
                            waveCoolDown = 5f;
                            waveDelayTimer = waveCoolDown;
                        }
                        if (waveCoolDown >= 50)
                        {
                            waveAmount = 10;
                        }
                        waveNumber++;
                    }
                }
            }
        }
    }
}