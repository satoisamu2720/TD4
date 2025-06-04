using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{

    // オブジェクトのリスト化
    [SerializeField]
    private GameObject[] objectKeep;

    private bool isKeep;

    private void Awake()
    {

        if (!isKeep)
        {

            foreach (GameObject obj in objectKeep)
            {
                DontDestroyOnLoad(obj);
            }

            DontDestroyOnLoad(gameObject);
            isKeep = true;
        }
        else
        {
            Destroy(gameObject);
        }

        // シーン切り替えのイベントに関数登録
        SceneManager.sceneLoaded += SceneTransition;

    }

    /// <summary>
    /// シーン移動時
    /// </summary>
    private void SceneTransition(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title") 
        {
            ResetObjects();
        }
    }

    /// <summary>
    /// オブジェクトのリセット
    /// </summary>
    private void ResetObjects()
    {
        foreach (var obj in objectKeep)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        Destroy(gameObject);
        isKeep = false;

    }

    /// <summary>
    /// 登録したイベント解除する処理
    /// </summary>
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneTransition;
    }

}
