using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro InstructionsTMP;
    [SerializeField] private TextMeshPro LevelTMP;
    [SerializeField] private PokeButton StartButton;
    [SerializeField] private PokeButton NextButton;
    [SerializeField] private PokeButton PrevButton;
    [SerializeField] private Transform InteractivesPivot;
    [SerializeField] private Vector2 InteractivesPivotHeightRange;
    [SerializeField] private NavGenator NavGenerator; 
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
        Debug.Log("Next level. curr: " +state);
        if (state == States.ONABOARDING)
        {
            StartLevel1();
        }
        else if (state == States.LEVEL_1_WINDOWS)
        {
            StartLevel2();
        }
        else if (state == States.LEVEL_2_FLOORS)
        {
            StartLevel3();
        }
    }

    public void StartOnboardingState()
    {
        state = States.ONABOARDING;
        InstructionsTMP.text = OnboardingInstructions;
        StartButton.transform.DOScale(1, 0.2f);
        StartButton.Cooldown();
        NextButton.transform.DOScale(0, 0.2f);
        PrevButton.transform.DOScale(0, 0.2f);
        LevelTMP.text = "INTRO";
    }

    public void StartLevel1()
    {
        Debug.Log("Start level 1");
        state = States.LEVEL_1_WINDOWS;
        InstructionsTMP.text = Level1Part1Instructions;
        StartButton.transform.DOScale(0, 0.2f);
        NextButton.transform.DOScale(1, 0.2f);
        NextButton.Cooldown();
        PrevButton.transform.DOScale(1, 0.2f);
        PrevButton.Cooldown();
        LevelTMP.text = "LEVEL 1 - WINDOWS";
    }

    public void StartLevel2()
    {
        Debug.Log("Start level 2");

        InstructionsTMP.text = Level2Part1Instructions;
        state = States.LEVEL_2_FLOORS;
        NextButton.transform.DOScale(1, 0.2f);
        NextButton.Cooldown();
        LevelTMP.text = "LEVEL 2 - FLOORS";
    }
    public void StartLevel3()
    {
        Debug.Log("Start level 3");

        InstructionsTMP.text = Level3Instructions;
        state = States.LEVEL_3_PATH;
        NextButton.transform.DOScale(0, 0.2f);
        LevelTMP.text = "LEVEL 3 - PATH";
        NavGenerator.CreateNav();
        PortalManager.Instance.ExplodeWallButCloseFloorPortals();
        MegaPortalManager.Instance.AnimateMegaPortalIn();
    }

    [Button]
    public void PrevLevel()
    {
        if (state == States.LEVEL_3_PATH) {
            PortalManager.Instance.CloseAllWallPortals();
            MegaPortalManager.Instance.AnimateMegaPortalAway();
            NavGenerator.Cleanup();
            StartLevel2();
        }
        else if (state == States.LEVEL_2_FLOORS)
        {
            PortalManager.Instance.CloseAllFloorPortals();
            StartLevel1();
        }
        else if (state == States.LEVEL_1_WINDOWS)
        {
            PortalManager.Instance.CloseAllPortals();
            StartOnboardingState();
        }
    }

    public void HeightSliderValueChanged(float v)
    {
        float y = Mathf.Lerp(InteractivesPivotHeightRange.x, InteractivesPivotHeightRange.y, v);
        InteractivesPivot.position = new Vector3(InteractivesPivot.position.x, y, InteractivesPivot.position.z);
    }
}
