using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyFollow : MonoBehaviour
{
    [SerializeField] private Transform Target ;
    [SerializeField] private float MinDistance = 2;
    [SerializeField] private float MaxDistance = 0.3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 flooredTargetPos = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        if (Vector3.Distance(flooredTargetPos,transform.position)>MinDistance)
        {
            transform.position = Vector3.Lerp(transform.position, flooredTargetPos, 0.01f);
        }
        
        if (Vector3.Distance(flooredTargetPos,transform.position)<MaxDistance)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, flooredTargetPos, -0.01f);
        }
        transform.LookAt(flooredTargetPos);
        transform.Rotate(Vector3.up,180);
    }
}
