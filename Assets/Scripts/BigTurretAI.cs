using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BigTurretAI : MonoBehaviour
{

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

    public float reloadCooldown = 2.5f;
    public int FireOrder = 1;
    private Transform turretHeadTransform;


    void Start()
    {
        turretHeadTransform = transform.Find("Turret Head");
    }

    void Update()
    {
        if (cur_target != null) {
            cur_target = FindNearestTarg();
        }

        if (cur_target != null) {
            float distance = Vector2.Distance(turretHeadTransform.position, cur_target.transform.position);
            if (attackMinimumDistance < distance && distance < attackMaximumDistance) {
                Vector3 vectorToTarget = turretHeadTransform.position - cur_target.transform.position;
                turretHeadTransform.rotation = Quaternion.Slerp(turretHeadTransform.rotation, Quaternion.LookRotation(vectorToTarget, Vector3.forward), TurretRotationSpeed * Time.deltaTime);
                turretHeadTransform.eulerAngles = new Vector3(0f, 0f, turretHeadTransform.eulerAngles.z);

                if (reloadTime > 0) {
                    reloadTime -= Time.deltaTime;
                }
                if (reloadTime < 0) {
                    reloadTime = 0;
                }
                if (reloadTime == 0) {

                    CauseDamage(5f, 25f);
                    reloadTime = reloadCooldown;
                }
                else {
                    cur_target = FindNearestTarg();
                }
            }
        }
    }
    public void CauseDamage(float min, float max)
    {
        turretHeadTransform.GetComponent<Animation>().Play("GunAttack");
        EnemyHP enemyhp = cur_target.GetComponent<EnemyHP>();
        if (enemyhp != null)
        {
            attackDamage = Random.Range(min, max);
            enemyhp.ReceiveDamage(attackDamage);
        }

    }

    public GameObject FindNearestTarg()
    {
        float closestEnDist = 0;
        GameObject nearestEnemy = null;
        List<GameObject> sortingMobs = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        float dist = 0;
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject everyTarget in targets) {
            dist = Vector3.Distance(everyTarget.transform.position, turretHeadTransform.position);
            if ((dist < closestEnDist) || closestEnDist == 0) {
                closestEnDist = Vector3.Distance(everyTarget.transform.position, turretHeadTransform.position);
                nearestEnemy = everyTarget;
            }
        }
        return (closestEnDist > attackMaximumDistance) ? null : nearestEnemy;
    }
}
