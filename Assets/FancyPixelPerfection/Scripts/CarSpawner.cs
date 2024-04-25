using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> CarPrefabs;

    [SerializeField] private BoxCollider BoxCollider;
    [SerializeField] private int CarCount = 40;
    [SerializeField] private float CarSpeed = 10;
    [SerializeField] private Vector3 Direction;
    private List<Transform> cars = new List<Transform>();
    void Start()
    {
        for (int i = 0; i < CarCount; i++)
        {
            var c = Instantiate(CarPrefabs[Random.Range(0, CarPrefabs.Count)],transform).transform;
            cars.Add(c);
            c.position = RandomPointInBounds(BoxCollider.bounds);
            c.gameObject.layer = LayerMask.NameToLayer("ThroughStencilOnly");
            //c.rotation = quaternion.identity;
        }
    }
    public static Vector3 RandomPointInBounds(Bounds bounds) {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
    void FixedUpdate()
    {
        foreach (var car in cars)
        {
            car.localPosition += new Vector3(-CarSpeed * Time.deltaTime,0,0 );
            if (car.localPosition.x < -2200) car.localPosition = new Vector3(2200, car.localPosition.y, car.localPosition.z);
        }
    }
}
