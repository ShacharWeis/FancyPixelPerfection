using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<PortalManager> Levels;
    [SerializeField] private TextMeshPro InstructionsTMP;
    [SerializeField] private PokeButton StartButton;
    [SerializeField] private PokeButton NextButton;
    [SerializeField] private PokeButton PrevButton;
    [TextArea(5,5)] [SerializeField] private string OnboardingInstructions;
    [TextArea(5,5)] [SerializeField] private string Level1Part1Instructions;
    [TextArea(5,5)] [SerializeField] private string Level2Part1Instructions;
    [TextArea(5,5)] [SerializeField] private string Level3Instructions;
    
    public enum States
    {
        ONABOARDING,
        LEVEL_1_WINDOWS,
        LEVEL_2_FLOORS,
        LEVEL_3_PATH
    }

    public void Start()
    {
        StartOnboardingState();
    }

    private States state;

    [Button]
    public void NextLevel()
    {
        if (state == States.ONABOARDING)
        {
            StartLevel1();
            Levels[0].OpenPortals();
        }
        else if (state == States.LEVEL_1_WINDOWS)
        {
            StartLevel2();
            Levels[1].OpenPortals();
        }
        else if (state == States.LEVEL_2_FLOORS)
        {
            StartLevel3();
            Levels[2].OpenPortals();
        }
    }

    public void StartOnboardingState()
    {
        state = States.ONABOARDING;
        InstructionsTMP.text = OnboardingInstructions;
        StartButton.transform.DOScale(1, 0.5f);
        NextButton.transform.DOScale(0, 0.5f);
        PrevButton.transform.DOScale(0, 0.5f);
    }
    
    public void StartLevel1()
    {
        state = States.LEVEL_1_WINDOWS;
        InstructionsTMP.text = Level1Part1Instructions;
        StartButton.transform.DOScale(0, 0.5f);
        NextButton.transform.DOScale(1, 0.5f);
        PrevButton.transform.DOScale(1, 0.5f);
    }
    
    public void StartLevel2()
    {
        InstructionsTMP.text = Level2Part1Instructions;
        state = States.LEVEL_2_FLOORS;
    }
    public void StartLevel3()
    {
        InstructionsTMP.text = Level3Instructions;
        state = States.LEVEL_3_PATH;
    }
    
    [Button]
    public void PrevLevel()
    {
        if (state == States.LEVEL_3_PATH)
        {
            StartLevel2();
            Levels[2].OpenPortals();
        }
        else if (state == States.LEVEL_2_FLOORS)
        {
            StartLevel1();
            Levels[1].OpenPortals();
        }
        else if (state == States.LEVEL_1_WINDOWS)
        {
            StartOnboardingState();
            Levels[0].ClosePortals();
        }
    }
    
}
