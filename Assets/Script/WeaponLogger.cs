using System.Collections.Generic;
using UnityEngine;

public class WeaponLogger : MonoBehaviour
{
    static List<string> parts = new List<string>();

    //リスト内の数をintで召喚出来る
    //if文等で使える
    public static int Count => parts.Count;

    public static bool Contains(string CheckStarPieceID)
    {
        return parts.Contains(CheckStarPieceID);
    }

    public static void Clear()
    {
        // アイテム数をクリア
        parts.Clear();
    }

    public static void Add(string GetStarPieceID)
    {
        if (Contains(GetStarPieceID))
        {
            //取得状態は追加するタイミングでもチェック!
            return;
        }
        // //ゲットしたアイテムを記録
        parts.Add(GetStarPieceID);
        Debug.Log(parts.Count);
    }
}
