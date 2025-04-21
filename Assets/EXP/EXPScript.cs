using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;


[CreateAssetMenu(menuName = "Data/Create LvData")] 
//プロジェクトビューで右クリックし→Data→Create LvDataと選択することで、Lvdataのassetファイルを作成
public class EXPScript : ScriptableObject
{
    //リスト宣言
    public List<PlayerExpTable> playerExpTables = new List<PlayerExpTable>();

    //クラスをインスペクターに表示
    [System.Serializable]

    //内部クラス（PlayerExpTabel）
    public class PlayerExpTable
    {
        public int level;
        public int exp;
    }

    
}
