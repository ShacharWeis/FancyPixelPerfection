using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class NavGenator : MonoBehaviour
{
    [SerializeField] private GameObject footTrackPrefab;
    [SerializeField] private GameObject finalGoalPrefab;
    [SerializeField] private float waypointDistance = 0.5f;
    [SerializeField] private Transform goal;
    public UnityEvent PathCreated;
    public UnityEvent PathCleanup;
    private NavMeshSurface navmesh;
    private NavMeshPath navMeshPath;

    private List<GameObject> footList = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshSurface>();
       // Invoke("CreateNav", 5f );
       // Invoke("CreatePath", 7f );
    }

    public void SetGoal(ref Transform target)
    {
        goal = target;
    }
    public void CreateNav()
    {
        navmesh.BuildNavMesh();
    }

    public void CreatePath()
    {
        navMeshPath = new NavMeshPath();
        Vector3 targetPoint = goal.transform.position;
          Vector3 startPoint = Camera.main.transform.position;
          startPoint.y = 0;
        NavMesh.CalculatePath(startPoint, targetPoint, NavMesh.AllAreas, navMeshPath);
        List<Vector3> waypoints = new List<Vector3>();
        for (int i = 0; i < navMeshPath.corners.Length; i++)
        {
            waypoints.Add(navMeshPath.corners[i]);
        }

      
        List<Vector3> interpolatedWaypoints = InterpolateWaypoints(waypoints, waypointDistance);
        
        for (int i = 1; i < interpolatedWaypoints.Count-1; i++)
        {
            Vector3 currentWaypoint = interpolatedWaypoints[i - 1];
            Vector3 nextWaypoint = interpolatedWaypoints[i];
            Vector3 direction = nextWaypoint - currentWaypoint;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            footList.Add(Instantiate(footTrackPrefab, nextWaypoint, rotation));
        }       
       // Vector3 finalWaypoint = interpolatedWaypoints[interpolatedWaypoints.Count - 1];
        //footList.Add(Instantiate(finalGoalPrefab, finalWaypoint, Quaternion.identity));
        PathCreated.Invoke();
        Debug.Log("Finished Creating path");
    }

    public void Cleanup()
    {
        foreach (GameObject Foot  in footList)
        {
            if(Foot != null)
                 Destroy(Foot);
        }
        footList.Clear();
        navMeshPath.ClearCorners();
        PathCleanup.Invoke();
    }
    
    List<Vector3> InterpolateWaypoints(List<Vector3> waypoints, float distance)
    {
        List<Vector3> interpolatedWaypoints = new List<Vector3>();

        if (waypoints.Count < 2)
        {
            return interpolatedWaypoints;
        }
        
        interpolatedWaypoints.Add(waypoints[0]);

        for (int i = 1; i < waypoints.Count; i++)
        {
            float segmentLength = Vector3.Distance(waypoints[i - 1], waypoints[i]);
            int numWaypoints = Mathf.FloorToInt(segmentLength / distance);
            float stepSize = 1f / numWaypoints;

            for (int j = 1; j <= numWaypoints; j++)
            {
                Vector3 interpolatedPoint = Vector3.Lerp(waypoints[i - 1], waypoints[i], stepSize * j);
                interpolatedWaypoints.Add(interpolatedPoint);
            }
        }

        return interpolatedWaypoints;
    }
    
}