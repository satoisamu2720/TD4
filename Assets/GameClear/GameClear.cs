using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Stage1Scene ‚É‘JˆÚ
            SceneTransitionManager.Instance.GoToTitleScene();
        }
    }

    public void OnAlpha1Button()
    {
        SceneTransitionManager.Instance.GoToTitleScene();
    }
}
