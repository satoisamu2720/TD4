using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement: MonoBehaviour
{
    public int PlayerHP = 0;

    public int BossEnemyHP = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
        PlayerHP = StetusScript.Instance.PlayerHp;
        BossEnemyHP = nWayBullet.Instance.currentHP;
        if (PlayerHP <= 0)
        {
            PlayerHP = 10;

            SceneManager.LoadScene("GameOver");
        }

        if (BossEnemyHP <= 0)
        {
          nWayBullet.Instance.Die();

          SceneManager.LoadScene("GameClear");
        }
    }
}
