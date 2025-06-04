using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StetusChange : MonoBehaviour
{

    [SerializeField] private string _LoadScene;

    int currentHp = StetusScript.Instance.PlayerHp;

    void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            StetusScript.Instance.PlayerHp += 10;
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(_LoadScene);
    }
}
