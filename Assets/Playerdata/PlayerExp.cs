using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class PlayerExp
{
    public string Name = "ålŒö";
    public ExpLevelClass ExpLevel = new ExpLevelClass();

    static readonly int[] TOTAL_EXP_ARRAY = { 0, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000,11000,12000,13000,14000,15000,16000 };

    //ŒoŒ±’lŠl“¾ˆ—
    public (int afterLevel, int remainExp) AddExp(int exp)
    {
        return ExpLevel.AddExp(exp, TOTAL_EXP_ARRAY);
    }

 
   
}

