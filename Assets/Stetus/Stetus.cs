using UnityEngine;

public class StetusScript : MonoBehaviour
{
   public static StetusScript Instance { get; private set; }

    public PlayerExp playerExp;

    //ステータス
    public int PlayerHp = 10;
    public int PlayerSpeed = 1;
    public int Bullet = 1;
    public int Exp;
    public int level;

    private void Update()
    {
        Exp = playerExp.ExpLevel.Exp;
        level = playerExp.ExpLevel.Level;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//シーンまたいでも大丈夫
        }
        else
        {
            Destroy(gameObject);//重複防止
        }
        
    }
}
