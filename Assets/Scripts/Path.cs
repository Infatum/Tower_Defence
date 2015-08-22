using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
    public List<Transform> Waypoints;
    public bool DrawGizmo;
    void Awake()
    {
        Waypoints.AddRange(gameObject.transform.GetComponentsInChildren<Transform>());
        Waypoints.RemoveAt(0); // Удаляем сам Path
        SortList();
        Waypoints[0].LookAt(Waypoints[1]); //Make first point look at the second

    }

    private static int CompareTransform1(Transform a, Transform b)
    {
        var f = int.Parse(a.name);
        var s = int.Parse(b.name);

        return f.CompareTo(s);
    }

    public void SortList()
    {
        Waypoints.Sort(CompareTransform1);
    }


    public List<Transform> GetPath()
    {
        return Waypoints;
    }

    void OnDrawGizmosSelected()
    {
        if (!DrawGizmo)
        {
            return;
        }

        Gizmos.color = Color.red;
        Waypoints.AddRange(gameObject.transform.GetComponentsInChildren<Transform>());
        Waypoints.RemoveAt(0);
        SortList();
        if (Waypoints != null && Waypoints.Count > 0)
        {

            for (int i = 1; i < Waypoints.Count; i++)
            {
                if (Waypoints[i - 1] != null && Waypoints[i] != null)
                {
                    Gizmos.DrawLine(Waypoints[i - 1].position, Waypoints[i].position);
                }
            }
        }
        Waypoints.Clear();
    }
}

