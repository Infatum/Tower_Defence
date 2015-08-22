using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public float enemyPrice = 5f;
    public float enemyMinSpeed = 0.5f;
    public float enemyMaxSpeed = 2f;
    public float enemyRotationSpeed = 2.5f;
    public float attackDistance = 5f;
    public float damage = 5f;
    public float attackTimer = 0f;
    public float coolDown = 2f;
    public float closestTargDist = 1f;
    public List<GameObject> pathPoints;
    int currentPathPoint = 0;

    private float enemyCurrentSpeed;
    private Transform enemy;
    private GlobalVars vars;

    private void Awake()
    {
        vars = GameObject.Find("GlobalVars").GetComponent<GlobalVars>();
        pathPoints = GameObject.FindGameObjectsWithTag("Path").OrderBy(GO => GO.name).ToList();
        enemy = transform;
        enemyCurrentSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
    }
    GameObject GetTarget()
    {
        if (Vector2.Distance(transform.position, pathPoints[currentPathPoint].transform.position) <= closestTargDist)
        {
            currentPathPoint++;
        }
        return pathPoints[currentPathPoint];
    }
    private GameObject NearestTurret()
    {
        float closestTurretDistance = 0;
        GameObject nearestTurret = null;
        List<GameObject> nearestTurrets = vars.TurretList;

        foreach (var turret in nearestTurrets)
        {
            if ((Vector2.Distance(enemy.position, target.transform.position) < closestTurretDistance) || closestTurretDistance == 0)
            {
                closestTurretDistance = Vector2.Distance(enemy.position, turret.transform.position);
                nearestTurret = turret;
            }
        }
        return nearestTurret;
    }


    void Update()
    {

        if (target != null)
        {
            target = GetTarget();

            //Rotate to Target
            Vector3 vectorToTarget = transform.position - target.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vectorToTarget, Vector3.forward),
                enemyRotationSpeed * Time.deltaTime);
           
            float distance = Vector2.Distance(target.transform.position, enemy.position);
            Vector2 structDirection = (target.transform.position - enemy.position).normalized;
            float attackDirection = Vector2.Dot(structDirection, enemy.forward);

            if (distance < attackDistance && attackDirection > 0)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                if (attackTimer <= 0)
                {
                    TurretHP targethp = target.GetComponent<TurretHP>();
                    if (targethp != null)
                    {
                        targethp.ChangeHP(-damage);
                    }
                    attackTimer = coolDown;
                }
            }
        }
    }
}
