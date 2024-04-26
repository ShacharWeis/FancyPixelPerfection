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
    [SerializeField] private GameObject footLPrefab;
    [SerializeField] private GameObject footRPrefab;
    [SerializeField] private GameObject finalGoalPrefab;
    [SerializeField] private float waypointDistance = 0.5f;
    [SerializeField] private float searchRadius = 3f;
    [SerializeField] private float fieldOfViewAngle = 90f;
    [SerializeField] private Vector3 goal;
    [SerializeField] private Material depthMaterial;
    public UnityEvent PathCreated;
    public UnityEvent PathCleanup;
    public UnityEvent BadLocation;
    private NavMeshSurface navmesh;
    private NavMeshPath navMeshPath;
    private GameObject MeshPath;
    private List<GameObject> footList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshSurface>();
        Invoke("CreateNav", 1f);
        //  Invoke("CreatePath", 2f);
    }

    public void SetGoal(ref Transform target)
    {
        goal = target.transform.position;
    }

    public void CreateNav()
    {
        navmesh.BuildNavMesh();

        if (!FindValidTarget())
        {
            BadLocation.Invoke();
        }
        else
        {
            CreatePath();
        }
    }

    public void CreatePath()
    {
        navMeshPath = new NavMeshPath();
        Vector3 targetPoint = goal;
        Vector3 startPoint = Camera.main.transform.position;
        startPoint.y = 0;
        NavMesh.CalculatePath(startPoint, targetPoint, NavMesh.AllAreas, navMeshPath);
        List<Vector3> waypoints = new List<Vector3>();
        for (int i = 0; i < navMeshPath.corners.Length; i++)
        {
            waypoints.Add(navMeshPath.corners[i]);
        }


        List<Vector3> interpolatedWaypoints = InterpolateWaypoints(waypoints, waypointDistance);

        for (int i = 1; i < interpolatedWaypoints.Count - 1; i++)
        {
            Vector3 currentWaypoint = interpolatedWaypoints[i - 1];
            Vector3 nextWaypoint = interpolatedWaypoints[i];
            Vector3 direction = nextWaypoint - currentWaypoint;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            nextWaypoint.y += 0.01f;
            if (i % 2 == 0)
            {
                footList.Add(Instantiate(footRPrefab, nextWaypoint, rotation));
            }
            else
            {
                footList.Add(Instantiate(footLPrefab, nextWaypoint, rotation));
            }
        }


        waypoints.Clear();
        float offset = 0.2f; // Adjust this value to control the amount of movement
        for (int i = 0; i < navMeshPath.corners.Length; i++)
        {
            Vector3 originalPoint = navMeshPath.corners[i];
            Vector3 movedPoint = new Vector3(originalPoint.x + offset, originalPoint.y, originalPoint.z);
            waypoints.Add(movedPoint);
        }

        Vector3 NewPoint = waypoints[0] - new Vector3(0.1f, 0, 0.1f);
        waypoints.Insert(0, NewPoint);

        CreateMesh(ref waypoints);
        // Vector3 finalWaypoint = interpolatedWaypoints[interpolatedWaypoints.Count - 1];
        //footList.Add(Instantiate(finalGoalPrefab, finalWaypoint, Quaternion.identity));
        PathCreated.Invoke();
        Debug.Log("Finished Creating path");
    }

    public void Cleanup()
    {
        foreach (GameObject Foot in footList)
        {
            if (Foot != null)
                Destroy(Foot);
        }

        footList.Clear();
        navMeshPath.ClearCorners();
        PathCleanup.Invoke();
        if (MeshPath)
        {
            Destroy(MeshPath);
        }
    }

    bool FindValidTarget()
    {
        Vector3 playerPosition = Camera.main.transform.position;
        Quaternion playerRotation = Camera.main.transform.rotation;


        int numberOfRays = 72; // Adjust the number of rays for smoother coverage

        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * 360f / numberOfRays - 180f; 
            Vector3 direction = Quaternion.Euler(0, angle, 0) * playerRotation * Vector3.forward;

            Debug.DrawRay(playerPosition, direction, Color.red, searchRadius);
            Vector3 pointInFront = playerPosition + direction * searchRadius;
            pointInFront.y = 0f;
            Debug.Log("Looking");
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(pointInFront, out navHit, searchRadius, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(navHit.position, playerPosition, NavMesh.AllAreas, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        goal = navHit.position;

                        Debug.Log("Found valid point on NavMesh: " + goal);
                        return true;
                    }
                }
            }
        }

        Debug.Log("Found no valid point on NavMesh: ");
        return false;
    }

    void CreateMesh(ref List<Vector3> points)
    {
        float meshWidth = 2f;

        MeshPath = new GameObject("FootMesh");
        MeshFilter meshFilter = MeshPath.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = MeshPath.AddComponent<MeshRenderer>();


        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();


        List<Vector3> outerPoints = new List<Vector3>();
        foreach (Vector3 point in points)
        {
            Vector3 direction = point - points[0];
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized;
            outerPoints.Add(point + perpendicular * meshWidth);
        }


        List<Vector3> combinedPoints = new List<Vector3>();
        combinedPoints.AddRange(points);
        combinedPoints.AddRange(outerPoints);


        for (int i = 0; i < points.Count; i++)
        {
            vertices.Add(points[i]);
            vertices.Add(outerPoints[i]);

            if (i > 0)
            {
                int baseIndex = i * 2;


                triangles.Add(baseIndex - 2);
                triangles.Add(baseIndex);
                triangles.Add(baseIndex - 1);

                triangles.Add(baseIndex - 1);
                triangles.Add(baseIndex);
                triangles.Add(baseIndex + 1);


                triangles.Add(baseIndex - 2);
                triangles.Add(baseIndex + 1);
                triangles.Add(baseIndex);

                triangles.Add(baseIndex - 1);
                triangles.Add(baseIndex + 1);
                triangles.Add(baseIndex - 2);
            }
        }


        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        meshFilter.mesh = mesh;


        MeshPath.transform.SetParent(null);
        MeshPath.AddComponent<MeshCollider>();
        Vector3 pos = MeshPath.transform.position;

        MeshPath.transform.position = pos;
        meshRenderer.material = depthMaterial;
        MeshPath.layer = LayerMask.NameToLayer("Stencil");
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