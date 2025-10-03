using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class PlayerExp
{
    public string Name = "ålŒö";
    public ExpLevelClass ExpLevel = new ExpLevelClass();

    // ŒoŒ±’lŠl“¾ˆ—
    public (int afterLevel, int remainExp) AddExp(int exp)
    {
        return ExpLevel.AddExp(exp);
    }
}

