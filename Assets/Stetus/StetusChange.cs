using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEditor;
using static WeaponSpawn.WeaponCount;

public class StetusChange : MonoBehaviour
{

    [SerializeField] private string _LoadScene;
    
    
    
    

    public Gun gun;


    void Start()
    {
        

    }




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

        int randomChoice = Random.Range(0, 3);
        StetusScript.Instance.level += 1;
        //StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
        //RsumeScene();
        

        switch (randomChoice)
        {
            case 0:
                StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
                Debug.Log("HP UP!");
                RsumeScene();
                break;
            case 1:
                StetusScript.Instance.PlayerSpeed -= StetusScript.Instance.LevelUpDashCoolTime;
                Debug.Log("CoolTimeDown!");
                RsumeScene();
                break;
            case 2:
                StetusScript.Instance.PlayerSpeed += StetusScript.Instance.LevelUpPlayerSpeed;
                Debug.Log("Speed UP!");
                RsumeScene();
                break;
        }

        
    }

    public void OnAlpha2Button()
    {
        int randomChoice2 = Random.Range(0, 3);

        string weaponID = Gun.Instance.GetCurrentWeaponID();
        Debug.Log("現在の武器ID: " + weaponID);
        StetusScript.Instance.level += 1;
        //StetusScript.Instance.PlayerSpeed -= StetusScript.Instance.LevelUpDashCoolTime;
        //RsumeScene();


        switch (weaponID)
        {
            case "handgun":


                switch (randomChoice2)
                {
                    case 0:
                        gun.reloadTime = StetusScript.Instance.handgunReloadTime -= 0.1f;
                        Debug.Log("リロード速度　UP hn");
                        RsumeScene();
                        break;
                    case 1:
                        gun.bulletLife = StetusScript.Instance.handgunBalletLife += 1;
                        Debug.Log("敵の貫通する数　UP hn");
                        RsumeScene();
                        break;
                    case 2:
                        StetusScript.Instance.PlayerSpeed -= 0.3f;
                        Debug.Log("弾の大きさ　UP hn");
                        RsumeScene();
                        break;

                }

                break;
            case "ar":
                switch (randomChoice2)
                {
                    case 0:
                        gun.reloadTime = StetusScript.Instance.ArReloadTime -= 0.1f;
                        Debug.Log("リロード速度　UP");
                        RsumeScene();
                        break;
                    case 1:
                        gun.bulletLife = StetusScript.Instance.ArBalletLife += 1;
                        Debug.Log("敵の貫通する数　UP");
                        RsumeScene();
                        break;
                    case 2:
                        StetusScript.Instance.PlayerSpeed -= 0.3f;
                        Debug.Log("弾の大きさ　UP");
                        RsumeScene();
                        break;

                }

                break;
            case "sg":

                switch (randomChoice2)
                {
                    case 0:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.reloadTime = StetusScript.Instance.SGReloadTime -= 0.3f;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }

                        Debug.Log("リロード速度　UP sg");
                        RsumeScene();
                        break;
                    case 1:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.bulletLife = StetusScript.Instance.SGBalletLife += 1;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }
                        Debug.Log("敵の貫通する数　UP sg");
                        RsumeScene();
                        break;
                    case 2:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.reloadTime = StetusScript.Instance.SGReloadTime -= 0.1f;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }
                        Debug.Log("弾の大きさ　UP sg");
                        RsumeScene();
                        break;

                }

                break;
        }




    }

    //武器関連
    public void OnAlpha3Button()
    {
        int randomChoice3 = Random.Range(0, 3);

        StetusScript.Instance.level += 1;
        //StetusScript.Instance.Bullet += StetusScript.Instance.LevelUpGunMagazine;
        RsumeScene();
        

        switch (randomChoice3)
        {
            case 0:
                StetusScript.Instance.PlayerSpeed -= 0.1f;
                Debug.Log("リロード速度　UP");
                RsumeScene();
                break;
            case 1:
                StetusScript.Instance.PlayerSpeed -= 0.2f;
                Debug.Log("敵の貫通する数　UP");
                RsumeScene();
                break;
            case 2:
                StetusScript.Instance.PlayerSpeed -= 0.3f;
                Debug.Log("弾の大きさ　UP");
                RsumeScene();
                break;

        }

        
    }

    public void RsumeScene()
    {
        //SceneTransitionManager.Instance.ReturnToStageScene();
        //Player.IsNotMove = false;

        StetusScript.Instance.isLevelUp = false;

        GameManagement.Instance.isPause = false;

    }


}
