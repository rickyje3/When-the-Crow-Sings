using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaypointsHolder : MonoBehaviour
{
    [HideInInspector]
    public List<EnemyWaypoint> waypoints;


    private void Awake()
    {
        waypoints = GetComponentsInChildren<EnemyWaypoint>().ToList();
        //Debug.Log("Waypoints are " + waypoints);
    }
    private void Start()
    {
        //waypoints = GetComponentsInChildren<EnemyWaypoint>().ToList();
        //Debug.Log("Waypoints are "+waypoints);
    }

    public EnemyWaypoint GetNextWaypoint(EnemyWaypoint currentWaypoint)
    {
        int currentIndex = waypoints.IndexOf(currentWaypoint);
        currentIndex += 1;
        if (currentIndex > waypoints.Count-1)
        {
            currentIndex = 0;
        }
        else if (currentIndex < 0)
        {
            currentIndex = waypoints.Count-1;
        }
        return waypoints[currentIndex];
    }
}
