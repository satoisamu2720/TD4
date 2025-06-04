using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
