using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PortalManager :  Singleton<PortalManager>
{
    private List<Portal> portals = new List<Portal>();
    [SerializeField] private float minPortalDistance = 2;
    [SerializeField] private Transform InteractivesPivot;
    [SerializeField] private AudioSource CityAudioSource;

    private const float PORTAL_SCALE_LOW = 0.5f;
    private const float PORTAL_SCALE_HIGH = 2.0f;

    public static PortalManager Instance
    {
        get
        {
            return ((PortalManager)mInstance);
        }
        set
        {
            mInstance = value;
        }
    }

    public void RegisterPortal(Portal p)
    {
        portals.Add(p);
        p.transform.parent = InteractivesPivot;
    }

    public bool CheckIfPositionIsAllowed(Vector3 p)
    {
        foreach (var portal in portals)
        {
            if (portal!=null && Vector3.Distance(p, portal.transform.position) < minPortalDistance)
                return false;
        }

        return true;
    }

    public void CloseAllPortals()
    {
        foreach (var portal in portals)
        {
            if (portal!=null) {
                portal.CloseAndDestroy();
            }
        }
        portals.Clear();
    }

    // This one's only used in a test scene, safe to trim probably.
    public void ExplodeAllPortals()
    {
        foreach (var portal in portals)
        {
            if (portal != null) {
                portal.ExplodeToInfinity();
            }
        }
    }

    public void CloseAllWallPortals() {
        foreach (var portal in portals) {
            if (portal is WallPortal) {
                portal.CloseAndDestroy();
            }
        }
    }
    public void CloseAllFloorPortals() {
        foreach (var portal in portals) {
            if (portal is FloorPortal) {
                portal.CloseAndDestroy();
            }
        }
    }
    public void ExplodeWallButCloseFloorPortals() {
        foreach (var portal in portals) {
            if (portal is WallPortal) {
                portal.ExplodeToInfinity();
            } else if (portal is FloorPortal) {
                portal.CloseAndDestroy();
            }
        }
    }

    public void StartHearingCityAudio() {
        CityAudioSource.DOFade(1f, 1f);
    }

    public void PortalSizeSliderValueChanged(float v)
    {
        float newScale = Mathf.Lerp(PORTAL_SCALE_LOW, PORTAL_SCALE_HIGH, v);
        foreach (var portal in portals) {
            if (portal != null) {
                portal.transform.localScale = newScale * Vector3.one;
            }
        }
    }
}