using System;
using UnityEngine;

public class StetusScript : MonoBehaviour
{
   public static StetusScript Instance { get; private set; }

    public PlayerExp playerExp;

    //ステータス
    public int PlayerHp = 10;
    public float PlayerSpeed = 1.0f;
    public int Bullet = 7;
    //public int Exp;
    public int level = 1;

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
