using UnityEngine;

public class SamWallTouchCollisionTest : MonoBehaviour {

    public GameObject holePrefab;

    void OnTriggerEnter(Collider other) {
        Debug.Log("Scene Model got OnTriggerEnter!");
        if (other.tag.Equals("Player")) {
            Debug.Log("Instantiating hole prefab!");
            Instantiate(holePrefab,
                GetComponent<Collider>().ClosestPoint(other.transform.position),
                Quaternion.identity);
        }
    }
}
