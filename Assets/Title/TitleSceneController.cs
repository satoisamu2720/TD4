using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleSceneController : MonoBehaviour
{
    private AudioSource bgmSource;
    public AudioClip getStartSE;
    AudioSource startSE;

    void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        startSE = GetComponent<AudioSource>();

        if (bgmSource != null)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void OnAlpha1Button()
    {
        StartCoroutine(PlaySEAndLoadScene(false));
    }

    public void OnAlpha2Button()
    {
        StartCoroutine(PlaySEAndLoadScene(true));
    }

    IEnumerator PlaySEAndLoadScene(bool loadFromSave)
    {
        if (startSE != null && getStartSE != null)
        {
            startSE.PlayOneShot(getStartSE);
            yield return new WaitForSeconds(1.0f); // 銃声が鳴り終わるまで待つ
            bgmSource.Stop();
        }


        PlayerPrefs.DeleteKey("PlayerUserData");
        if (loadFromSave)
        {
            PlayerPrefs.SetInt("StartLoad", 1);
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            PlayerPrefs.DeleteKey("StartLoad");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Stage1");
        }


    }

    // 音量調整
    public void SetBgmVolume(float volume)
    {
        if (bgmSource != null)
            bgmSource.volume = Mathf.Clamp01(volume);

        if (startSE != null)
            startSE.volume = 1.0f; // 効果音は固定（必要なら調整可能）
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
