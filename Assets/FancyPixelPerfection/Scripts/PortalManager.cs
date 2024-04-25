using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PortalManager :  Singleton<PortalManager>
{
    private List<Portal> portals = new List<Portal>();
    [SerializeField] private float minPortalDistance = 2;
    [SerializeField] private Transform InteractivesPivot;
    [SerializeField] private AudioSource CityAudioSource;

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
            if (portal!=null)
                portal.CloseAndDestroy();
        }
        portals.Clear();
    }

    public void ExplodeAllPortals()
    {
        foreach (var portal in portals)
        {
            portal.ExplodeToInfinity();
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
}