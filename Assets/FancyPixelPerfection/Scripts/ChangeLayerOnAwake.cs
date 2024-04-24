using UnityEngine;

public class ChangeLayerOnAwake : MonoBehaviour {
    public string layerName = "ThroughStencilOnly";
    private LayerMask _layerMask;

    void Awake()
    {
        _layerMask =  LayerMask.NameToLayer(layerName);
        foreach (Transform t in transform) {
            ChangeLayerOnSelfAndChildren(t);
        }
    }
    private void ChangeLayerOnSelfAndChildren(Transform t) {
        // Change this transform's layer!
        t.gameObject.layer = _layerMask;

        // Do it on the children!
        foreach (Transform t2 in t) {
            ChangeLayerOnSelfAndChildren(t2);
        }
    }
}
