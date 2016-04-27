using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Enemy AI class
    /// </summary>
    [SerializeField]
    private float enemyMinSpeed = 0.1f;
    [SerializeField]
    private float enemyMaxSpeed = 0.5f;
    [SerializeField]
    private float enemyRotationSpeed = 2.5f;
    [SerializeField]
    private float closestTargetDistance = 2f;
    [SerializeField]
    private List<GameObject> pathPoints;

    float enemyCurrentSpeed;
    GameObject target;
    int currentPathPoint = 0;

    /// <summary>
    /// Sets the enemy current random speed for each enemy in a scene;
    /// Picks pathpoints to build a peth for an enemy to move on;
    /// Generates random path for each spawn waves of enemies;
    /// </summary>
    void Awake()
    {
        enemyCurrentSpeed = Random.Range(enemyMinSpeed, enemyMaxSpeed);
        bool path1 = Random.value >= 0.5;
        pathPoints = GameObject.FindGameObjectsWithTag((path1) ? "Path" : "Path2").OrderBy(go => go.name).ToList();
        if (pathPoints.Count == 0)
        {
            pathPoints = GameObject.FindGameObjectsWithTag("Path").OrderBy(go => go.name).ToList();
        }
    }
    /// <summary>
    /// Update method per frame. Moves enemy by path.Rotates target to a path point;
    /// Destroys enemy if he reaches last point in Path Points;
    /// </summary>
    void Update()
    {
        try
        {
            target = GetNearestPoint();
        }
        catch(System.ArgumentOutOfRangeException)
        {
            Destroy(gameObject);
            GameControll.ChangePlayerHP(-10);
            return;
        }
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
    /// <summary>
    /// Gets the nearest pathpoint to which moves on
    /// </summary>
    /// <returns></returns>
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
        return pathPoints[currentPathPoint];
    }

}