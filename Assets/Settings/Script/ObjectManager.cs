using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{

    // オブジェクトのリスト化
    [SerializeField]
    private GameObject[] objectKeep;
    [SerializeField]
    private GameObject[] sceneSpecificDestroyObjects;
    private bool isKeep;
    private Dictionary<GameObject, bool> destroyedFlags = new();

    private void Awake()
    { 
        if (!isKeep)
        {
            foreach (GameObject obj in objectKeep)
            {
                DontDestroyOnLoad(obj);
            }

            foreach (GameObject obj in sceneSpecificDestroyObjects)
            {
                DontDestroyOnLoad(obj);
                destroyedFlags[obj] = false; // 初期化
            }

            DontDestroyOnLoad(gameObject);
            isKeep = true;

            SceneManager.sceneLoaded += SceneTransition;
        }
        else
        {
            Destroy(gameObject);
        }
    }
        /// <summary>
        /// シーン移動時
        /// </summary>
    private void SceneTransition(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title") 
        {
            ResetObjects();
        }else if (scene.name == "Stage1")// 
        {
            DestroySceneSpecificObjects();
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

        foreach (var obj in sceneSpecificDestroyObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        destroyedFlags.Clear();
        isKeep = false;
        Destroy(gameObject);
    }

    /// <summary>
    /// 登録したイベント解除する処理
    /// </summary>
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneTransition;
       
    }

    private void DestroySceneSpecificObjects()
    {
        foreach (var obj in sceneSpecificDestroyObjects)
        {
            if (obj != null && !destroyedFlags[obj])
            {
                Destroy(obj);
                destroyedFlags[obj] = true;
            }
        }
    }

}
