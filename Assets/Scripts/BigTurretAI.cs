﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BigTurretAI : MonoBehaviour
{
    /// <summary>
    /// AI of the game turret
    /// </summary>
    [SerializeField]
    private GameObject curTarget;
    [SerializeField]
    private float currentHP;
    [SerializeField]
    private float baseTurretRotationSpeed;
    [SerializeField]
    private float towerPrice = 100.0f;
    [SerializeField]
    private float attackMaximumDistance = 50.0f;
    [SerializeField]
    private float attackMinimumDistance = 1.0f;
    [SerializeField]
    private float seingMaximumDistance = 1.8f;
    [SerializeField]
    private float attackDamage = 10.0f;
    [SerializeField]
    private float reloadTIME = 2.5f;
    [SerializeField]
    private float turretRotationSpeed = 1.5f;
    [SerializeField]
    private float reloadCooldown = 2.5f;
    [SerializeField]
    private int FireOrder = 1;
    [SerializeField]
    private GameObject turretHead;

    /// <summary>
    /// Start method, finding turretHead in Turret game objects
    /// </summary>
    void Start()
    {
        turretHead = transform.GetChild(0).gameObject; 
    }
    /// <summary>
    /// Update method per frame. Searches for the nearest target to attack, if finds it, calculates distance between target and turret gun;
    /// Rotates the gun in a way to a target;
    /// Attacks the target;
    /// Animates the gun attack;
    /// </summary>
    void Update()
    {
        curTarget = FindNearestTarg();

        if (curTarget != null)
        {
            float distance = Vector2.Distance(turretHead.transform.position, curTarget.transform.position);
            if (attackMinimumDistance < distance && distance < attackMaximumDistance)
            {
                Vector3 vectorToTarget = turretHead.transform.position - curTarget.transform.position;
                turretHead.transform.rotation = Quaternion.Slerp(turretHead.transform.rotation, Quaternion.LookRotation(vectorToTarget, Vector3.forward), turretRotationSpeed * Time.deltaTime);
                turretHead.transform.eulerAngles = new Vector3(0f, 0f, turretHead.transform.eulerAngles.z);

                if (reloadTIME > 0)
                {
                    reloadTIME -= Time.deltaTime;
                }
                if (reloadTIME < 0)
                {
                    reloadTIME = 0;
                }
                if (reloadTIME == 0)
                {
                    turretHead.GetComponent<Animation>().Stop("GunFireAnimation");

                    CauseDamage(attackDamage, attackDamage + 5f);
                    reloadTIME = reloadCooldown;
                }
                else
                {
                    curTarget = FindNearestTarg();
                }
            }
        }
    }

    /// <summary>
    /// Causes damage to the target in range of minimal and maximal damage value;
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public void CauseDamage(float min, float max)
    {
        turretHead.GetComponent<Animation>().Play("GunAttack");
        turretHead.transform.GetChild(0).gameObject.GetComponent<Animator>().StartPlayback();
        EnemyHP enemyhp = curTarget.GetComponent<EnemyHP>();
        if (enemyhp != null)
        {
            attackDamage = Random.Range(min, max);
            enemyhp.ReceiveDamage(attackDamage);
        }
    }
    /// <summary>
    /// Search for enemy;
    /// </summary>
    /// <returns>GameObject of a nearest target(enemy)</returns>
    public GameObject FindNearestTarg()
    {
        float closestEnDist = 0;
        GameObject nearestEnemy = null;
        List<GameObject> sortingMobs = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        float dist = 0;
        foreach (GameObject everyTarget in sortingMobs)
        {
            dist = Vector3.Distance(everyTarget.transform.position, turretHead.transform.position);
            if ((dist < closestEnDist) || closestEnDist == 0)
            {
                closestEnDist = Vector3.Distance(everyTarget.transform.position, turretHead.transform.position);
                nearestEnemy = everyTarget;
            }
        }
        return (closestEnDist > seingMaximumDistance) ? null : nearestEnemy;
    }
}
