using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    private AudioSource bgmSource;

    void Start()
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
            SceneManager.LoadScene("Stage1");
            bgmSource.Stop();
        }
    }

    public void OnAlpha1Button()
    {
        PlayerPrefs.DeleteKey("PlayerUserData");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Stage1");
        bgmSource.Stop();
    }
    public void OnAlpha2Button()
    {
        PlayerPrefs.DeleteKey("PlayerUserData");
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("StartLoad", 1);
        SceneManager.LoadScene("Stage1");
        bgmSource.Stop();
    }

    // 音量を設定（0.0f〜1.0fの範囲）
    public void SetBgmVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = Mathf.Clamp01(volume);
        }
    }
}
