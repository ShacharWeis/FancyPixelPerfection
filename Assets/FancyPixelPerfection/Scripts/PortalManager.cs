using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
}
