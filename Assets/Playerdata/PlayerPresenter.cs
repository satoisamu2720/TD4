using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerExp playerExp;

    public int ExpBox;
    public int ExpBox2;

    [SerializeField] int _currentValue = 1;
    [SerializeField] private string _LoadScene;

    public int CurrentValue => _currentValue;
    public static bool IsNotMove { get; set; } = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentValue != playerExp.ExpLevel.Level)
        {
            if (_currentValue < playerExp.ExpLevel.Level)
            {
                StopScene();
                Debug.Log("ì¸Ç¡ÇΩ");
                _currentValue = playerExp.ExpLevel.Level;
            }
        }
    }

    public void StopScene()
    {
        //IsNotMove = true;
        //SceneManager.LoadScene(_LoadScene, LoadSceneMode.Additive);
        Debug.Log("é~ÇﬂÇΩ");
        
        StopManager.Instance.Pause();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ìñÇΩÇ¡ÇΩ");
        if (other.CompareTag("ExpItem"))
        {
            ExpItem item = other.GetComponent<ExpItem>();
            if (item != null)
            {
                playerExp.AddExp(item.expAmount);
                Destroy(other.gameObject); // ÉAÉCÉeÉÄÇè¡Ç∑
            }
        }
    }
}
