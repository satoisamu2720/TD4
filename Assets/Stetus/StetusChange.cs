using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StetusChange : MonoBehaviour
{

    //[SerializeField] private string _LoadScene;
    GameObject levelUpPanel;
    
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
            RsumeScene();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            StetusScript.Instance.level += 1;
            StetusScript.Instance.PlayerSpeed -= StetusScript.Instance.LevelUpDashCoolTime;
            RsumeScene();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            StetusScript.Instance.level += 1;
            //StetusScript.Instance.Bullet += StetusScript.Instance.LevelUpGunMagazine;
            RsumeScene();
        }
    }

    public void OnAlpha1Button()
    {
        StetusScript.Instance.level += 1;
        StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
        RsumeScene();
    }

    public void OnAlpha2Button()
    {
        StetusScript.Instance.level += 1;
        StetusScript.Instance.PlayerSpeed -= StetusScript.Instance.LevelUpDashCoolTime;
        RsumeScene();
    }

    public void OnAlpha3Button()
    {
        StetusScript.Instance.level += 1;
        //StetusScript.Instance.Bullet += StetusScript.Instance.LevelUpGunMagazine;
        RsumeScene();
    }

    public void RsumeScene()
    {
        //SceneTransitionManager.Instance.ReturnToStageScene();
        //Player.IsNotMove = false;

        GameManagement.Instance.isLevelUp = false;

        GameManagement.Instance.isPause = false;

    }


}
