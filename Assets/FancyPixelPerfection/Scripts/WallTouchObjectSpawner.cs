using UnityEngine;

public class WallTouchObjectSpawner : MonoBehaviour {

    public GameObject holePrefab;

    void OnTriggerEnter(Collider other) {
        Debug.Log("WallTouchObjectSpawner got OnTriggerEnter!");

        if (other.tag.Equals("Player")) {
            Debug.Log("Touch came from Player, instantiating prefab!");

            Vector3 pos = GetComponent<Collider>().ClosestPoint(other.transform.position);
            Quaternion rot = Quaternion.LookRotation(Camera.main.transform.forward);
            Instantiate(holePrefab, pos, rot);
        }
    }
}
