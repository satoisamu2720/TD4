using UnityEngine;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 2ŒÂ–Ú‚ð–h‚®
        }
    }
}
