using System.Collections.Generic;
using UnityEngine;

public class WeaponLogger : MonoBehaviour
{
    static List<string> parts = new List<string>();

    //リスト内の数をintで召喚出来る
    //if文等で使える
    public static int Count => parts.Count;

    // 指定したIDがリストに含まれているか確認
    public static bool Contains(string CheckWeaponID)
    {
        return parts.Contains(CheckWeaponID);
    }

    public static void Clear()
    {
        // アイテム数をクリア
        parts.Clear();
    }

    // 新しい武器IDを追加（既にある場合はスキップ）
    public static void Add(string GetWeaponID)
    {
        if (Contains(GetWeaponID))
        {
            // 既に取得済みの武器IDは追加しない
            return;
        }
        // //ゲットしたアイテムを記録
        parts.Add(GetWeaponID);
        Debug.Log(parts.Count);
    }
}
