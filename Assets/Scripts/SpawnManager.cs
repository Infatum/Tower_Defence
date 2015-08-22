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
    public Transform enemy;
    public GameObject[] SpawnPoints;
    public GameObject[] pathPoints;
    private GlobalVars vars;

    private void Awake()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        vars = GameObject.Find("GlobalVars").GetComponent<GlobalVars>();
    }

    private void Update()
    {
        if (waveDelayTimer > 0)
        {
            if (vars != null)
            {
                if(vars.EnemyCount == 0)
                {
                    waveDelayTimer = 0;
                }
                else
                {
                    waveDelayTimer -= Time.deltaTime;
                }
            }
        }
        if (waveDelayTimer <= 0)
        {
            if (SpawnPoints != null && waveNumber < maxWaves)
            {
                foreach(GameObject spawnpoint in SpawnPoints)
                {
                    for (int i = 0; i < waveAmount; i++)
                    {
                        Instantiate(enemy, new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y + i * 0.5f, spawnpoint.transform.position.z), Quaternion.identity);
                    }
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
