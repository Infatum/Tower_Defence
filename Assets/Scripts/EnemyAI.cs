using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

    public GameObject target;
    public float enemyPrice = 5f;
    public float enemyMinSpeed = 0.5f;
    public float enemyMaxSpeed = 2f;
    public float enemyRotationSpeed = 2.5f;
    public float attackDistance = 5f;
    public float damage = 5f;
    public float attackTimer = 0f;
    public float coolDown = 2f;

    private float enemyCurrentSpeed;
    private Transform enemy;
    private GlobalVars vars;

    private void Awake()
    {
        vars = GameObject.Find("GlobalVars").GetComponent<GlobalVars>();
        enemy = transform;
        enemyCurrentSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
    }
    private GameObject NearestTurret()
    {
        float closestTurretDistance = 0;
        GameObject nearestTurret = null;
        List<GameObject> nearestTurrets = vars.TurretList;

        foreach(var turret in nearestTurrets)
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

        if (target == null)
        {
            target = NearestTurret();
        }
        else
        {
            enemy.rotation = Quaternion.Lerp(enemy.rotation,
                Quaternion.LookRotation(new Vector3(0.0f, 0.0f, target.transform.position.z)
                -
                new Vector3(0.0f, 0.0f, enemy.position.z)),
                enemyRotationSpeed);

            enemy.position += enemy.forward * enemyCurrentSpeed * Time.deltaTime;

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
