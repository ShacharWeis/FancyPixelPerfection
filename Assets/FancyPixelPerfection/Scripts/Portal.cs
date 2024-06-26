using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Portal : MonoBehaviour {
    public Transform thingToScale;

    public void CloseAndDestroy()
    {
        GetComponent<Animator>().StopPlayback();
        transform.DOScale(0, 0.3f).OnComplete(() => Destroy(gameObject));
    }

    public void ExplodeToInfinity() {
        GetComponent<Animator>().Play("ScaleOutInfinityBounce");
    }

    public void DoScaling(float newScale) {
        thingToScale.localScale = Vector3.one * newScale;
    }
}