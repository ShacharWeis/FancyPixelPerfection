using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class AssignMeshColliderMeshWhenWeHaveIt : MonoBehaviour {
    private MeshFilter _mf;
    private bool _created;

    void Awake() {
        _mf = GetComponent<MeshFilter>();
    }
    void Update() {

        // This is expensive, but hopefully only happens for a few frames...
        if (_created || _mf.sharedMesh == null) return;

        Debug.Log("MeshFilter has a mesh! " + _mf.sharedMesh);

        // Get the collider and assign the mesh!
        GetComponent<MeshCollider>().sharedMesh = _mf.sharedMesh;

        // As soon as this happens, flip a bool so it doesn't happen again.
        _created = true;
    }
}