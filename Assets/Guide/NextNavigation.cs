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
            TutorialStepController.Instance.ProgressToNextStep();
            var followUI = UIFollowWorldObject.GetInstance();
            if (followUI != null)
            {
                followUI.ShowUI(true);
            }

        }
    }
}
