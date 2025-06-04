using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class PlayerExp
{
    public string Name = "ålŒö";
    public ExpLevelClass ExpLevel = new ExpLevelClass();

    static readonly int[] TOTAL_EXP_ARRAY = { 0, 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000 };

    //ŒoŒ±’lŠl“¾ˆ—
    public (int afterLevel, int remainExp) AddExp(int exp)
    {
        return ExpLevel.AddExp(exp, TOTAL_EXP_ARRAY);
    }

 
   
}

