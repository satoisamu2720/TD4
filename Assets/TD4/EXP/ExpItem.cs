using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public static ExpItem Instance { get; private set; }
    public int expAmount = 1000;
    public bool isCollected = false;

    public void Collect(PlayerExp playerExp)
    {
        if (!GameManagement.Instance.StartTutorialLeveUp) 
        { 
            expAmount = 250;
            StetusScript.Instance.EnemyHp = 3;
        }
        
        Destroy(gameObject);         // é©ï™Çè¡Ç∑
        if (isCollected)
        {
            return;
        }
        playerExp.AddExp(expAmount); // åoå±ílÇâ¡éZ
        isCollected = true;
        

    }

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
