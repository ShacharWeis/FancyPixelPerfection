using UnityEngine;

public class SimpleRotate : MonoBehaviour {
    public float speed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.up, speed);
    }
}
