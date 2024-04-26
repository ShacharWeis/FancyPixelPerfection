using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

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
    [SerializeField] private GameObject FootstepsHolder;
    [SerializeField] private GameObject HeightSliderPanel;
    [SerializeField] private GameObject WindowSizeSliderPanel;
    [TextArea(5,5)] [SerializeField] private string OnboardingInstructions;
    [TextArea(5,5)] [SerializeField] private string Level1Part1Instructions;
    [TextArea(5,5)] [SerializeField] private string Level2Part1Instructions;
    [TextArea(5,5)] [SerializeField] private string Level3Instructions;

    public States startingState;
    
    private int _footStepCounter;

    public enum States
    {
        ONABOARDING,
        LEVEL_1_WINDOWS,
        LEVEL_2_FLOORS,
        LEVEL_3_PATH
    }

    void Awake() {
        State = startingState;        
    }
    
    public void Start()
    {
        StartOnboardingState();
    }

    public static States State;

    [Button]
    public void NextLevel()
    {
        Debug.Log("Next level. curr: " +State);
        if (State == States.ONABOARDING)
        {
            StartLevel1();
        }
        else if (State == States.LEVEL_1_WINDOWS)
        {
            StartLevel2();
        }
        else if (State == States.LEVEL_2_FLOORS)
        {
            StartLevel3();
        }
    }

    public void StartOnboardingState()
    {
        State = States.ONABOARDING;
        InstructionsTMP.text = OnboardingInstructions;
        StartButton.transform.DOScale(1, 0.2f);
        StartButton.Cooldown();
        NextButton.transform.DOScale(0, 0.2f);
        PrevButton.transform.DOScale(0, 0.2f);
        HeightSliderPanel.transform.DOScale(0, 0);
        WindowSizeSliderPanel.transform.DOScale(0, 0);
        LevelTMP.text = "INTRO";
    }

    public void StartLevel1()
    {
        Debug.Log("Start level 1");
        State = States.LEVEL_1_WINDOWS;
        InstructionsTMP.text = Level1Part1Instructions;
        StartButton.transform.DOScale(0, 0.2f);
        NextButton.transform.DOScale(1, 0.2f);
        NextButton.Cooldown();
        PrevButton.transform.DOScale(1, 0.2f);
        PrevButton.Cooldown();
        HeightSliderPanel.transform.DOScale(1, 0.5f);
        WindowSizeSliderPanel.transform.DOScale(1, 0.5f);
        LevelTMP.text = "LEVEL 1 - WINDOWS";
    }

    public void StartLevel2()
    {
        Debug.Log("Start level 2");

        InstructionsTMP.text = Level2Part1Instructions;
        State = States.LEVEL_2_FLOORS;
        NextButton.transform.DOScale(1, 0.2f);
        NextButton.Cooldown();
        WindowSizeSliderPanel.transform.DOScale(1, 0.5f);
        LevelTMP.text = "LEVEL 2 - FLOORS";
    }
    public void StartLevel3()
    {
        Debug.Log("Start level 3");

        InstructionsTMP.text = Level3Instructions;
        State = States.LEVEL_3_PATH;
        NextButton.transform.DOScale(0, 0.2f);
        WindowSizeSliderPanel.transform.DOScale(0, 0.5f);
        LevelTMP.text = "LEVEL 3 - PATH";
        StartCoroutine(Level3Stuff());
    }

    [Button]
    public void PrevLevel()
    {
        if (State == States.LEVEL_3_PATH) {
            PortalManager.Instance.CloseAllWallPortals();
            MegaPortalManager.Instance.AnimateMegaPortalAway();
           // NavGenerator.Cleanup();
            StartLevel2();
        }
        else if (State == States.LEVEL_2_FLOORS)
        {
            PortalManager.Instance.CloseAllFloorPortals();
            StartLevel1();
        }
        else if (State == States.LEVEL_1_WINDOWS)
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

    IEnumerator Level3Stuff() {
        PortalManager.Instance.ExplodeWallButCloseFloorPortals();
        yield return new WaitForSeconds(1f);
        MegaPortalManager.Instance.AnimateMegaPortalIn();
        yield return new WaitForSeconds(0.5f);
        //NavGenerator.CreateNav(); // TODO - Scott, you got this!
        // Fallback for now:
        FootstepsHolder.SetActive(true);
    }

    public void OnFootstep() {
        _footStepCounter++;

        if (_footStepCounter == 5) {
            Debug.Log("You win!");
        }
    }
}