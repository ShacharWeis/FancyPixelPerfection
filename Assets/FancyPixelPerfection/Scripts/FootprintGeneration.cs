using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class FootprintGeneration : MonoBehaviour
{
    [SerializeField] private GameObject footIK =null; 
    [SerializeField] private GameObject footprintPrefab = null;
    [SerializeField] private string floorTag = "Floor";
    private RaycastHit[] hits = new RaycastHit[1];
    private bool cooldownCreation = false;
    
    void Update()
    {
        if (IsFootTouchingGround(ref footIK))
        {
            SpawnFootprint( footIK.transform.position );
        }
    }
    
    bool IsFootTouchingGround(ref GameObject foot)
    {
        
            int hitCount = Physics.RaycastNonAlloc(foot.transform.position, Vector3.down, hits, 0.1f);
            Debug.DrawRay(foot.transform.position, Vector3.down * 0.1f, Color.red);
            if (hitCount > 0)
            {
                
               
                if (hits[0].collider.CompareTag(floorTag))
                {
                    return true;
                }
            }
            return false;
    }

    void SpawnFootprint( Vector3 position)
    {
        if (cooldownCreation)
            return;
        
        Instantiate(footprintPrefab, position, footIK.transform.rotation);
        cooldownCreation = true;
        Invoke("ResetCoolDown", 3f);
    }

    void ResetCoolDown()
    {
        cooldownCreation = false;
    }
}
