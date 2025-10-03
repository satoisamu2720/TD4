using UnityEngine;

public class NextNavigation : MonoBehaviour
{
    public string playerTag = "Player";

    private bool NextFlag = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && NextFlag)
        {
            NextFlag = false;
            StetusScript.Instance.Save();
            TutorialStepController.Instance.ProgressToNextStep();
            if (UIFollowWorldObject.Instance != null)
            {
                UIFollowWorldObject.Instance.ShowUI(true);
            }
            GameManagement.Instance.StartTutorialLeveUp = false;

        }
    }
}
