using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour
{
    [SerializeField] ExpLevelClass levelClass;
    [SerializeField] int _currentValue = 1;

    public int CurrentValue => _currentValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentValue != levelClass.Level)
        {
            if(_currentValue > levelClass.Level)
            {
                //SceneManager.LoadScene();
                _currentValue = levelClass.Level;
            }
        }
    }
}
