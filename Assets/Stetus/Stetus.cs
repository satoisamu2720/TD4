using System;
using UnityEngine;

public class StetusScript : MonoBehaviour
{
   public static StetusScript Instance { get; private set; }

    public PlayerExp playerExp;

    [Header("プレイヤーステータス")]
    public int PlayerHp = 5;
    public float PlayerSpeed = 1.0f;
    public int Bullet = 7;
    //public int Exp;
    public int level = 1;

    [Header("ボスステータス")]
    public int BossEnemyHp = 0;

    [Header("レベルアップステータス")]
    public int LevelUpPlayerHp = 2;
    public float LevelUpDashCoolTime = 0.05f;
    public int LevelUpGunMagazine = 1;


    private void Update()
    {
        
        //Exp = playerExp.ExpLevel.Exp;
        //level = playerExp.ExpLevel.Level;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject); 
        }
    }

    internal void ResetStatus()
    {
        throw new NotImplementedException();
    }
}
