using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Stage1Scene ‚É‘JˆÚ
            SceneManager.LoadScene("Title");
        }
    }

    public void OnAlpha1Button()
    {
        SceneManager.LoadScene("Title");
    }
}