using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StetusChange : MonoBehaviour
{

    [SerializeField] private string _LoadScene;

    
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
            ChangeScene();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.PlayerSpeed -= StetusScript.Instance.LevelUpDashCoolTime;
            ChangeScene();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.hundgunFireInterval -= 0.2f;
            ChangeScene();
        }
    }

    public void OnAlpha1Button()
    {
        StetusScript.Instance.level += 1;
        StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
        ChangeScene();
    }

    public void OnAlpha2Button()
    {
        StetusScript.Instance.level += 1;
        StetusScript.Instance.PlayerSpeed -= StetusScript.Instance.LevelUpDashCoolTime;
        ChangeScene();
    }

    public void OnAlpha3Button()
    {
        StetusScript.Instance.level += 1;
        StetusScript.Instance.hundgunFireInterval -= 0.2f;
        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneTransitionManager.Instance.ReturnToStageScene();
        Player.IsNotMove = false;
    }


}
