using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public int expAmount = 10;
    public bool isCollected = false;

    public void Collect(PlayerExp playerExp)
    {
        Destroy(gameObject);         // ©•ª‚ğÁ‚·
        if (isCollected)
        {
            return;
        }
        playerExp.AddExp(expAmount); // ŒoŒ±’l‚ğ‰ÁZ
        isCollected = true;
        
        
        
        // ‚·‚Å‚Éæ‚ç‚ê‚Ä‚¢‚ê‚Î‰½‚à‚µ‚È‚¢

    }
}
