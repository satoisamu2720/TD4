using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement: MonoBehaviour
{
    private int PlayerHP = 0;

    void Start()
    {
        PlayerHP = StetusScript.Instance.PlayerHp;
    }

    
    void Update()
    {
       
        if (StetusScript.Instance.PlayerHp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (nWayBullet.Instance != null && nWayBullet.Instance.currentHP <= 0)
        {
            SceneManager.LoadScene("GameClear");
        }
    }
}
