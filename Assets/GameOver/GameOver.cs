using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
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
        PlayerPrefs.DeleteKey("PlayerUserData");
        PlayerPrefs.Save();
    }
    public void OnAlpha2Button()
    {

        PlayerPrefs.SetInt("StartLoad", 1);
        SceneManager.LoadScene("Stage1");
    }
}