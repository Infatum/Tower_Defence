using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BigTurretAI : MonoBehaviour {

    public GameObject[] targets;
    public GameObject cur_target;
    public float CurrentHP;
    public float BaseTurretRotationSpeed;
    //public GameObject TurretObject;
    public float towerPrice = 100.0f;
    public float attackMaximumDistance = 50.0f;
    public float attackMinimumDistance = 1.0f;
    public float seingMaximumDistance = 1.8f;
    public float attackDamage = 10.0f;
    public float reloadTime = 2.5f;
    public const float ReloadTime = 2.5f;
    public float TurretRotationSpeed = 1.5f;
    public bool targetInLOS;

    public float reloadCooldown = 2.5f;
    public int FireOrder = 1;
    public GameObject turretHead;

    
    void Start()
	{
        turretHead = GameObject.Find("BigTurret");
        //GameObject turretHead_GO = GameObject.Find("BigTurret");
        //if (!turretHead)
        //{
        //    Debug.LogError("ERROR: Script 'BigTurretAI.cs' disabled, can't find turretHead");
        //    gameObject.SetActive(false);
        //} else
        //{
        //    turretHead = turretHead_GO.transform;
        //}
    }

	void Update()
	{
        if (cur_target != null)
        {
            float distance = Vector2.Distance(turretHead.transform.position, cur_target.transform.position);
            if (attackMinimumDistance < distance && distance < attackMaximumDistance)
            {
                Quaternion rotator = Quaternion.LookRotation(transform.position - cur_target.transform.position, Vector3.forward);
                transform.rotation = rotator;
                Debug.Log("rotation");
                transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z);
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
                            FireOrder++;
                            break;
                        case 2:
                            Debug.Log("Second turret fire");
                            FireOrder = 1;
                            break;
                    }
                    reloadTime = reloadCooldown;
                }
                else
                {
                    cur_target = SortTargets();
                }
            }
        }
    }
    public GameObject SortTargets()
    {
        float closestEnDist = 0;
        GameObject nearestEnemy = null;
        List<GameObject> sortingMobs = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        foreach(var everyTarget in sortingMobs)
        {
            if((Vector2.Distance(everyTarget.transform.position, turretHead.transform.position) < closestEnDist) || closestEnDist == 0)
            {
                closestEnDist = Vector2.Distance(everyTarget.transform.position, turretHead.transform.position);
                nearestEnemy = everyTarget;
            }
        }
        return nearestEnemy;
    }
}
