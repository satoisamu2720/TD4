using UnityEngine;

public class SceneLeave : MonoBehaviour
{

    private static SceneLeave instance;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Ç±ÇÍÇ≈ÉVÅ[ÉìÇÇ‹ÇΩÇ¢Ç≈Ç‡écÇÈ
    }
}
