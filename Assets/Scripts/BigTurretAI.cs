using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BigTurretAI : MonoBehaviour
{
    /// <summary>
    /// AI of the game turret
    /// </summary>
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
    private GameObject turretHead;

    /// <summary>
    /// Start method, finding turretHead in Turret game objects
    /// </summary>
    void Start()
    {
        turretHead = transform.GetChild(0).gameObject; 
    }
    /// <summary>
    /// Update method per frame. Searches for the nearest target to attack, if finds it calculates distance between target and turret gun;
    /// Rotates the gun in a way to a target;
    /// Attacks the target;
    /// Animates the gun attack;
    /// </summary>
    void Update()
    {
        cur_target = FindNearestTarg();

        if (cur_target != null)
        {
            float distance = Vector2.Distance(turretHead.transform.position, cur_target.transform.position);
            if (attackMinimumDistance < distance && distance < attackMaximumDistance)
            {
                Vector3 vectorToTarget = turretHead.transform.position - cur_target.transform.position;
                turretHead.transform.rotation = Quaternion.Slerp(turretHead.transform.rotation, Quaternion.LookRotation(vectorToTarget, Vector3.forward), TurretRotationSpeed * Time.deltaTime);
                turretHead.transform.eulerAngles = new Vector3(0f, 0f, turretHead.transform.eulerAngles.z);

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
                    turretHead.GetComponent<Animation>().Stop("GunFireAnimation");

                    CauseDamage(5f, 25f);
                    reloadTime = reloadCooldown;
                }
                else
                {
                    cur_target = FindNearestTarg();
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
        EnemyHP enemyhp = cur_target.GetComponent<EnemyHP>();
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
        return (closestEnDist > attackMaximumDistance) ? null : nearestEnemy;
    }
}
