using UnityEngine;

public class WallTouchObjectSpawner : MonoBehaviour {

    public GameObject holePrefab;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("WallTouchObjectSpawner got OnTriggerEnter!");

        if (other.tag.Equals("Player"))
        {
            Debug.Log("Touch came from Player!");

            // Only allow portals in the middle states, not beginning or level 3
            if (GameManager.State is GameManager.States.LEVEL_1_WINDOWS or GameManager.States.LEVEL_2_FLOORS) {
                Vector3 pos = GetComponent<Collider>().ClosestPoint(other.transform.position);

                // Only allow portals a certain distance from each other
                if (PortalManager.Instance.CheckIfPositionIsAllowed(pos)) {
                    Quaternion rot = Quaternion.LookRotation(transform.forward);
                    var p = Instantiate(holePrefab, pos, rot).GetComponent<Portal>();
                    PortalManager.Instance.RegisterPortal(p);
                }
            }
        }
    }
}
