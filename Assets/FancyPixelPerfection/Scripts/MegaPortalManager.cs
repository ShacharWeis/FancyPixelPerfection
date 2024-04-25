using DG.Tweening;
using UnityEngine;

public class MegaPortalManager : MonoBehaviour {
    public Transform megaPortal;

    public void AnimateMegaPortalIn() {
        megaPortal.DOLocalMove(new Vector3 (0,0,0), 1);
    }
    public void AnimateMegaPortalAway() {
        megaPortal.DOLocalMove(new Vector3 (0,1,0), 1);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("AnimateMegaPortalIn");
            AnimateMegaPortalIn();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("AnimateMegaPortalAway");
            AnimateMegaPortalAway();
        }
    }
}