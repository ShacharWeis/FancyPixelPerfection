using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<PortalManager> Levels;
    public enum States
    {
        ONABOARDING,
        LEVEL_1_WINDOWS,
        LEVEL_2_FLOORS,
        LEVEL_3_PATH
    }

    private States state;

    [Button]
    public void NextLevel()
    {
        if (state == States.ONABOARDING)
        {
            state = States.LEVEL_1_WINDOWS;
            Levels[0].OpenPortals();
        }
        else if (state == States.LEVEL_1_WINDOWS)
        {
            state = States.LEVEL_2_FLOORS;
            Levels[1].OpenPortals();
        }
        else if (state == States.LEVEL_2_FLOORS)
        {
            state = States.LEVEL_3_PATH;
            Levels[2].OpenPortals();
        }
    }
    
    [Button]
    public void PrevLevel()
    {
        if (state == States.LEVEL_3_PATH)
        {
            state = States.LEVEL_2_FLOORS;
            Levels[2].OpenPortals();
        }
        else if (state == States.LEVEL_2_FLOORS)
        {
            state = States.LEVEL_1_WINDOWS;
            Levels[1].OpenPortals();
        }
        else if (state == States.LEVEL_1_WINDOWS)
        {
            state = States.ONABOARDING;
            Levels[0].ClosePortals();
        }
    }
}
