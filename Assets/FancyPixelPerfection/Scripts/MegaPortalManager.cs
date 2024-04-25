using DG.Tweening;
using UnityEngine;

public class MegaPortalManager : Singleton<MegaPortalManager> {

    public static MegaPortalManager Instance
    {
        get
        {
            return ((MegaPortalManager)mInstance);
        }
        set
        {
            mInstance = value;
        }
    }

    public Transform[] megaPortals;

    public void AnimateMegaPortalIn() {
        foreach (Transform t in megaPortals) {
            t.DOScale(1, 2);
        }
    }
    public void AnimateMegaPortalAway() {
        foreach (Transform t in megaPortals) {
            t.DOScale(0, 1);
        }
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