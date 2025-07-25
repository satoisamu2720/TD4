using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEditor;
using static WeaponSpawn.WeaponCount;

public class StetusChange : MonoBehaviour
{
    public TextMeshProUGUI messageText; // 表示するメッセージテキスト
    [SerializeField] private string _LoadScene;
    int[] randomChoices = new int[3];  // 3ボタン分の選択肢

    int randomChoice;



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
        if(randomChoice == 0)
        {
            messageText.text = "プレイヤーのHP + 2";
           
        }
        if(randomChoice == 1)
        {
            messageText.text = "プレイヤーのダッシュクールタイム - 0.1秒";
        }
        if(randomChoice == 2)
        {
            messageText.text = "プレイヤーの移動スピード + 1";
        }
    }
    public void PrepareLevelUpOptions()
    {
        for (int i = 0; i < 3; i++)
        {
            randomChoices[i] = Random.Range(0, 3);  // 各ボタンの内容をランダムで決定
        }

        // 表示内容を決定
        string[] descriptions = new string[3];
        for (int i = 0; i < 3; i++)
        {
            descriptions[i] = GetDescription(randomChoices[i]);
        }

        // 例：messageText にまとめて表示（分けたい場合は別のTextもOK）
        messageText.text =
            $"① {descriptions[0]}\n" +
            $"② {descriptions[1]}\n" +
            $"③ {descriptions[2]}";
    }
    public void OnAlpha1Button()
    {
        if (!GameManagement.Instance.tutorialLeveUpPlate) return;

        ApplyLevelUp(randomChoices[0]);
    }
    void ApplyLevelUp(int choice)
    {
        StetusScript.Instance.level += 1;

        switch (choice)
        {
            case 0:
                StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
                break;
            case 1:
                StetusScript.Instance.PlayerDashCoolTime -= StetusScript.Instance.LevelUpDashCoolTime;
                break;
            case 2:
                StetusScript.Instance.PlayerSpeed += StetusScript.Instance.LevelUpPlayerSpeed;
                break;
        }

        RsumeScene();
    }
    string GetDescription(int choice)
    {
        switch (choice)
        {
            case 0: return "HP +2";
            case 1: return "ダッシュクールタイム -0.1秒";
            case 2: return "移動速度 +1";
            default: return "";
        }
    }
    //public void OnAlpha1Button()
    //{
    //    if (!GameManagement.Instance.tutorialLeveUpPlate)
    //    {
    //        return;
    //    }
    //    randomChoice = Random.Range(0, 3);
    //    StetusScript.Instance.level += 1;
    //    StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
    //    RsumeScene();


    //    switch (randomChoice)
    //    {
    //        case 0:
    //            StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
    //            Debug.Log("HP UP!");

    //            RsumeScene();
    //            break;
    //        case 1:
    //            StetusScript.Instance.PlayerDashCoolTime -= StetusScript.Instance.LevelUpDashCoolTime;
    //            Debug.Log("CoolTimeDown!");

    //            RsumeScene();
    //            break;
    //        case 2:
    //            StetusScript.Instance.PlayerSpeed += StetusScript.Instance.LevelUpPlayerSpeed;
    //            Debug.Log("Speed UP!");

    //            RsumeScene();
    //            break;
    //    }


    //}

    public void OnAlpha3Button()
    {
        if (!GameManagement.Instance.tutorialLeveUpPlate)
        {
            return;
        }
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
                        gun.reloadTime = StetusScript.Instance.HandgunReloadTime -= 0.1f;
                        Debug.Log("リロード速度　UP hn");
                        RsumeScene();
                        break;
                    case 1:
                        gun.bulletLife = StetusScript.Instance.HandgunBalletLife += 1;
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
    public void OnAlpha2Button()
    {
        if (!GameManagement.Instance.tutorialLeveUpPlate)
        {
            return;
        }
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

        GameManagement.Instance.isLevelUp = false;

        GameManagement.Instance.isPause = false;

    }


}
