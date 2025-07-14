using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{

    
    public static SceneTransitionManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // ステータスシーンへ移動（ステージは残す）
    public void GoToStatusScene()
    {
        var stageRoot = GameObject.Find("StageRoot");
        if (stageRoot != null) stageRoot.SetActive(false);

        SceneManager.LoadScene("StetusScene", LoadSceneMode.Additive);
    }

    // ステージに戻る
    public void ReturnToStageScene()
    {
        SceneManager.UnloadSceneAsync("StetusScene");

        var stageRoot = GameObject.Find("StageRoot");
        if (stageRoot != null) stageRoot.SetActive(true);
    }

    // タイトルに戻る（全Scene初期化）
    public void GoToTitleScene()
    {
        //StetusScript.Instance.ResetStatus();
        SceneManager.LoadScene("Title"); // LoadMode.Single で初期化される
    }
}
