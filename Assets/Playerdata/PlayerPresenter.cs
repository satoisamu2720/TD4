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

        if (Input.GetKey(KeyCode.Space))
        {
            ExpBox += 10;
            playerExp.AddExp(ExpBox);
            ExpBox = ExpBox2;
            ExpBox2 = 0;
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(_LoadScene);
    }


    //public void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        ExpBox += 10;
    //        playerExp.AddExp(ExpBox);
    //        ExpBox = ExpBox2;
    //        ExpBox2 = 0;
    //    }

        
    //}
}
