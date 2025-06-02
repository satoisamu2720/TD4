using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]

public class ExpLevelClass
{
    [SerializeField] int _exp;
    [SerializeField] int _remainExp;
    [SerializeField] int _minLevel;
    [SerializeField] int _level = 1;

    public int Exp => _exp;
    public int RemainExp => _remainExp;
    public int MinLevel => _minLevel;
    public int Level => _level;

    //Expを加算してlvを初期化する
    public (int afterLevel, int remainExp) AddExp(int exp, int[] expArray)
    {
        //カンストを考慮して加算
        _exp = Mathf.Clamp(_exp + exp, 0, expArray[expArray.Length - 1]);
        //値の更新
        UpdateLevel(expArray);
        UpdateRemainExp(expArray);

        return (Level, RemainExp);
    }

    void UpdateLevel(int[] expArray) 
    {
        //現Exp以下の値の中で最大のインデックスを取得
        var macIdx = expArray.Where(x => x <= _exp).Select((val, idx) => new { V = val, I = idx })
            .Aggregate((max, working) => (max.V > working.V) ? max : working).I;

        _level = macIdx + 1;
    }

    void UpdateRemainExp(int[] expArray)
    {
        //現Expより大きい値の中で最小の値のインデックスを取得
        var minIdx = expArray.Where(x => x > _exp).Select((val, idx) => new {V = val, I = idx})
            .Aggregate((min, working) => (min.V < working.V) ? min : working).I;

        _remainExp = expArray[minIdx] - _exp;
    }

    
}
