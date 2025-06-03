using UnityEngine;

public class TutorialStepController : MonoBehaviour
{
    public ArrowNavigation arrowNavigation;
    public Transform[] tutorialTargets;

    private int currentStep = 0;

    void Start()
    {
        if (tutorialTargets.Length > 0)
        {
            arrowNavigation.SetTarget(tutorialTargets[0]);
        }
    }

    void Update()
    {
        // Nキーで次の目的地へ強制的に切り替え
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentStep++;
            if (currentStep < tutorialTargets.Length)
            {
                //Debug.Log("Switching to target step: " + currentStep);
                arrowNavigation.SetTarget(tutorialTargets[currentStep]);
            }
            else
            {
                //Debug.Log("Tutorial completed.");
                arrowNavigation.SetTarget(null); // 矢印非表示
            }
        }
    }
}
