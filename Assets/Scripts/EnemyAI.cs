using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class EnemyAI : MonoBehaviour
{
    public float enemyMinSpeed = 0.1f;
    public float enemyMaxSpeed = 0.5f;
    public float enemyRotationSpeed = 2.5f;
    public float closestTargetDistance = 2f;

    float enemyCurrentSpeed;
    GameObject target;
    public List<GameObject> pathPoints;
    int currentPathPoint = 0;

    void Awake()
    {
        enemyCurrentSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
        bool path1 = Random.value >= 0.5;
        
        pathPoints = GameObject.FindGameObjectsWithTag((path1) ? "Path" : "Path2").OrderBy(go => go.name).ToList();
    }

    void Update()
    {
        target = GetNearestPoint();
        if (target == null)
        {
            return;
        }
        // Rotate to target
        Vector3 vectorToTarget = transform.position - target.transform.position;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(vectorToTarget, Vector3.forward),
            enemyRotationSpeed * Time.deltaTime
        );
        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z);

        // Move to up
        transform.position += transform.up * enemyCurrentSpeed * Time.deltaTime;
    }
    //Gets the nearest pathpoint to which moves on
    GameObject GetNearestPoint()
    {
        
            if (Vector2.Distance(transform.position, pathPoints[currentPathPoint].transform.position) <= closestTargetDistance)
            {
                currentPathPoint++;
            }
        
        if (currentPathPoint == pathPoints.Count)
        {
            Destroy(gameObject);
            GameControll.ChangePlayerHP(-10);
        }
        try {
            return pathPoints[currentPathPoint];
        }
        catch (System.ArgumentOutOfRangeException)
        {
            Destroy(gameObject);
            GameControll.ChangePlayerHP(-10);
            return null;
        }
    }

}