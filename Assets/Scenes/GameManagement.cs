using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement: MonoBehaviour
{
    

    void Start()
    {
        
    }

    
    void Update()
    {
       
        if (StetusScript.Instance.PlayerHp <= 0)
        {
            StetusScript.Instance.PlayerHp = 10;

            nWayBullet.Instance.Die();

            SceneManager.LoadScene("GameOver");
        }

        //if (nWayBullet.Instance.currentHP <= 0)
        //{
           

        //    SceneManager.LoadScene("GameClear");
        //}
    }
}
