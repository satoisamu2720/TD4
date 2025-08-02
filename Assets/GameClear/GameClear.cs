using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    private AudioSource bgmSource;

    private void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        if (bgmSource != null)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Stage1Scene ‚É‘JˆÚ
            SceneTransitionManager.Instance.GoToTitleScene();
            bgmSource.Stop();
        }
    }

    public void OnAlpha1Button()
    {
        SceneTransitionManager.Instance.GoToTitleScene();
        bgmSource.Stop();
    }
}
