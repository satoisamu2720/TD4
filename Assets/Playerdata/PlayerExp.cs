using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class PlayerExp
{
    public string Name = "ålŒö";
    public ExpLevelClass ExpLevel = new ExpLevelClass();

    static readonly int[] TOTAL_EXP_ARRAY = { 0, 100, 300, 600, 1000 };

    //ŒoŒ±’lŠl“¾ˆ—
    public (int afterLevel, int remainExp) AddExp(int exp)
    {
        return ExpLevel.AddExp(exp, TOTAL_EXP_ARRAY);
    }

 
   
}

