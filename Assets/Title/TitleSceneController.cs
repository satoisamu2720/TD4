using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Stage1Scene ‚É‘JˆÚ
            SceneManager.LoadScene("Stage1");
        }
    }

    public void OnAlpha1Button()
    {
        SceneManager.LoadScene("Stage1");
    }
}
