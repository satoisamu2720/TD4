using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerExp playerExp;

    public int ExpBox;
    public int ExpBox2;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ExpBox += 10;
            playerExp.AddExp(ExpBox);
            ExpBox = ExpBox2;
            ExpBox2 = 0;
        }

        
    }
}
