using System.Collections.Generic;
using UnityEngine;

public class PortalManager :  Singleton<PortalManager>
{
    private List<Portal> portals = new List<Portal>();
    [SerializeField] private float minPortalDistance = 2;
    [SerializeField] private Transform InteractivesPivot;
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
            if (Vector3.Distance(p, portal.transform.position) < minPortalDistance)
                return false;
        }

        return true;
    }

    public void CloseAllPortals()
    {
        foreach (var portal in portals)
        {
            portal.CloseAndDestroy();
        }
        portals.Clear();;
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
}