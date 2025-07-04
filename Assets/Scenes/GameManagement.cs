using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement: MonoBehaviour
{
    private int PlayerHP = 0;
    private bool oneBoss = false;

    void Start()
    {
        PlayerHP = StetusScript.Instance.PlayerHp;
    }

    
    void Update()
    {
       
        if (StetusScript.Instance != null && StetusScript.Instance.PlayerHp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (nWayBullet.Instance != null && nWayBullet.Instance.currentHP <= 0 && !oneBoss)
        {
            PlayerMove.Instance.transform.position = new Vector3(-160,-20,0);

            StetusScript.Instance.Save();
            oneBoss = true;
            nWayBullet.Instance.Die();

        }
    }
}
