using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioClip getDieSE;
    AudioSource zonbiSE;
    AudioSource dieSE;
    private void Start()
    {
       
        zonbiSE = GetComponent<AudioSource>();
        dieSE = GetComponent<AudioSource>();
        if (zonbiSE != null)
        {
            zonbiSE.loop = true;
            zonbiSE.Play();
            dieSE.PlayOneShot(getDieSE);
        }
    }
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
        if (zonbiSE != null)
        {            
            zonbiSE.Stop();
        }
        SceneTransitionManager.Instance.GoToTitleScene();
        PlayerPrefs.DeleteKey("PlayerUserData");
        PlayerPrefs.Save();
    }
    public void OnAlpha2Button()
    {
        if (zonbiSE != null)
        {
           zonbiSE.Stop();
        }
        PlayerPrefs.SetInt("StartLoad",2);
        SceneManager.LoadScene("Stage1");
    }
    public void OnAlpha3Button()
    {
        if (zonbiSE != null)
        {
            zonbiSE.Stop();
        }
        PlayerPrefs.SetInt("StartLoad", 0);
        SceneManager.LoadScene("Stage1");
    }

    public void SetBgmVolume(float volume)
    {
        if (zonbiSE != null)
            zonbiSE.volume = Mathf.Clamp01(volume);

        if (dieSE != null)
            dieSE.volume = Mathf.Clamp01(volume); 
    }

}