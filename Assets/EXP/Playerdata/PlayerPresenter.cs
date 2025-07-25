using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPresenter : MonoBehaviour
{
    public PlayerExp playerExp;

    public static PlayerPresenter Instance { get; private set; }

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
                StopScene();
                Debug.Log("“ü‚Á‚½");
                _currentValue = playerExp.ExpLevel.Level;
            }
        }
    }

    public void StopScene()
    {
        //SceneManager.LoadScene(_LoadScene, LoadSceneMode.Additive);
        Debug.Log("Ž~‚ß‚½");
        GameManagement.Instance.isLevelUp = true;
        GameManagement.Instance.isPause = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExpItem"))
        {
            ExpItem item = other.GetComponent<ExpItem>();
            if (item != null)
            {
                item.Collect(playerExp);
            }
        }
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
