using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PokeSlider : MonoBehaviour
{
    [SerializeField] private Transform Knob;
    [SerializeField] private Transform LowEnd;
    [SerializeField] private Transform HighEnd;
    [SerializeField] UnityEvent<float> ValueChanged;
    void OnTriggerStay(Collider other)
    {

        if (other.tag.Equals("FingerTip"))
        {

            Vector3 pos = GetComponent<Collider>().ClosestPoint(other.transform.position);
            float v = InverseLerp(LowEnd.position, HighEnd.position, pos);
            float localX = Mathf.Lerp(LowEnd.localPosition.x, HighEnd.localPosition.x, v);
            Knob.transform.localPosition = new Vector3(localX,Knob.localPosition.y, Knob.localPosition.z);
            ValueChanged.Invoke(v);
        }
    }

    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }

}
