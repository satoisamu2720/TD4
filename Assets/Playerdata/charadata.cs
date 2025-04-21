using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//属性
[CreateAssetMenu(menuName = "Data/Create StatusData")]
public class charadata : ScriptableObject
{
    public string NAME; //名前
    public int MAXHP;   //最大体力
    public int ATK;     //攻撃力
    public int AGI;     //速度
    public int LV;      //レベル
    public int GETEXP;  //取得経験値
    public int GETGOLD; //取得お金
}
