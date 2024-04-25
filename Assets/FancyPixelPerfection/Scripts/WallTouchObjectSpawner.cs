using UnityEngine;

public class WallTouchObjectSpawner : MonoBehaviour {

    public GameObject holePrefab;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("WallTouchObjectSpawner got OnTriggerEnter!");

        if (other.tag.Equals("Player"))
        {
            Debug.Log("Touch came from Player, instantiating prefab!");

            Vector3 pos = GetComponent<Collider>().ClosestPoint(other.transform.position);
            if (PortalManager.Instance.CheckIfPositionIsAllowed(pos))
            {
                Quaternion rot = Quaternion.LookRotation(transform.forward);
                var p = Instantiate(holePrefab, pos, rot).GetComponent<Portal>();
                PortalManager.Instance.RegisterPortal(p);
            }

        }
    }
}
