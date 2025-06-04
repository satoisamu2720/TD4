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
                ChangeScene();
                _currentValue = playerExp.ExpLevel.Level;
            }
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(_LoadScene);
    }


    private void OnTriggerEnter(Collider other)
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
