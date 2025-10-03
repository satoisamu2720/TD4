using UnityEngine;

public class TutorialStepController : MonoBehaviour
{
    public ArrowNavigation arrowNavigation;
    public Transform[] tutorialTargets;

    private int currentStep = 0;

    
    public static TutorialStepController Instance { get; private set; }

void Start()
    {
        if (tutorialTargets.Length > 0)
        {
            arrowNavigation.SetTarget(tutorialTargets[0]);
        }
    }

    void Update()
    {
        // Nキーで次の目的地に進む処理
        if (Input.GetKeyDown(KeyCode.N))
        {
            ProgressToNextStep();
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // チュートリアルステップを進めるメソッド
    public void ProgressToNextStep()
    {
        currentStep++;
        if (currentStep < tutorialTargets.Length)
        {
            Debug.Log("Switching to target step: " + currentStep);
            arrowNavigation.SetTarget(tutorialTargets[currentStep]);
        }
        else
        {
            Debug.Log("Tutorial completed.");
            arrowNavigation.SetTarget(null); // 矢印非表示
        }
    }
    public int GetCurrentStep()
    {
        return currentStep;
    }

    public void SetStep(int step)
    {
        currentStep = Mathf.Clamp(step, 0, tutorialTargets.Length - 1);
        if (tutorialTargets.Length > 0)
        {
            arrowNavigation.SetTarget(tutorialTargets[currentStep]);
        }
    }
}
