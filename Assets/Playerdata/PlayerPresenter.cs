using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerExp playerExp;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerExp.AddExp(10);
        }
    }
}
