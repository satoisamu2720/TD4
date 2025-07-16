using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour
{
    public PlayerExp playerExp;
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
        if(_currentValue != playerExp.ExpLevel.Level)
        {
            if(_currentValue < playerExp.ExpLevel.Level)
            {
                StopScene();
                //_currentValue = levelClass.Level;
            }
        }
    }

    public void StopScene()
    {
        //SceneManager.LoadScene(_LoadScene);
        GameManagement.Instance.isLevelUp = true;
        //StopManager.Instance.Pause();
    }

}
