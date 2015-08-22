﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BigTurretAI : MonoBehaviour {

    public GameObject[] targets;
    public GameObject cur_target;
    public float CurrentHP;
    public float BaseTurretRotationSpeed;
    public float towerPrice = 100.0f;
    public float attackMaximumDistance = 50.0f;
    public float attackMinimumDistance = 1.0f;
    public float seingMaximumDistance = 1.8f;
    public float attackDamage = 10.0f;
    public float reloadTime = 2.5f;
    public const float ReloadTime = 2.5f;
    public float TurretRotationSpeed = 1.5f;
    public float rotationSpeed = 1.5f;

    public float reloadCooldown = 2.5f;
    public int FireOrder = 1;
    private Transform turretHeadTransform;

    
    void Start()
	{
        turretHeadTransform = transform.Find("Turret Head");
    }

	void Update()
	{
        if (cur_target != null)
        {
            cur_target = FindNearestTarg();
        }

        if (cur_target != null)
        {
            float distance = Vector2.Distance(turretHeadTransform.position, cur_target.transform.position);
            if (attackMinimumDistance < distance && distance < attackMaximumDistance)
            {
                Vector3 vectorToTarget = turretHeadTransform.position - cur_target.transform.position;
                turretHeadTransform.rotation = Quaternion.Slerp(turretHeadTransform.rotation, Quaternion.LookRotation(vectorToTarget, Vector3.forward), 1 * Time.deltaTime);
                turretHeadTransform.eulerAngles = new Vector3(0f, 0f, turretHeadTransform.eulerAngles.z);
                Debug.Log("rotation");
           
                if (reloadTime > 0)
                {
                    reloadTime -= Time.deltaTime;
                }
                if (reloadTime < 0)
                {
                    reloadTime = 0;
                }
                if (reloadTime == 0)
                {
                    EnemyHP enemyhp = cur_target.GetComponent<EnemyHP>();
                    switch (FireOrder)
                    {
                        case 1:
                            Debug.Log("First turret fire");
                            if (enemyhp != null)
                            {
                                enemyhp.ChangeHP(-attackDamage);
                            }
                            FireOrder++;
                            break;
                        case 2:
                            Debug.Log("Second turret fire");
                            if (enemyhp != null)
                            {
                                enemyhp.ChangeHP(-attackDamage + 5f);
                            }
                            FireOrder = 1;
                            break;
                    }
                    reloadTime = reloadCooldown;
                }
                else
                {
                    cur_target = FindNearestTarg();
                }
            }
        }
    }

    public GameObject FindNearestTarg()
    {
        float closestEnDist = 0;
        GameObject nearestEnemy = null;
        //List<GameObject> sortingMobs = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        float dist = 0;
        if (targets.Length < 1)
        {
            targets = GameObject.FindGameObjectsWithTag("Enemy");
        }

        foreach (GameObject everyTarget in targets)
        {
            print(everyTarget.gameObject.name);
            dist = Vector3.Distance(everyTarget.transform.position, turretHeadTransform.position);
            if ((dist < closestEnDist) || closestEnDist == 0)
            {
                closestEnDist = Vector3.Distance(everyTarget.transform.position, turretHeadTransform.position);
                nearestEnemy = everyTarget;
            }
        }
        return (closestEnDist > attackMaximumDistance) ? null : nearestEnemy;
    }
}
