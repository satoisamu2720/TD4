using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEditor;
using static WeaponSpawn.WeaponCount;
using System.Collections.Generic;

public class StetusChange : MonoBehaviour
{
    public static StetusChange Instance { get; private set; }
    public TextMeshProUGUI[] levelUpTexts = new TextMeshProUGUI[3];
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
    }
    public void PrepareLevelUpOptions()
    {
        randomChoices[0] = Random.Range(0, 3);
        randomChoices[1] = Random.Range(3, 6);    

        List<int> mixCandidates = new List<int>() { 0, 1, 2, 3, 4, 5 };
        mixCandidates.Remove(randomChoices[0]);
        mixCandidates.Remove(randomChoices[1]);

        randomChoices[2] = mixCandidates[Random.Range(0, mixCandidates.Count)]; // Mix

        // 各選択肢に応じた説明文を取得
        string[] descriptions = new string[3];
        descriptions[0] = GetPlayerDescription(randomChoices[0]);
        descriptions[1] = GetGunDescription(randomChoices[1]);
        descriptions[2] = GetMixDescription(randomChoices[2]);

        // すべてのTextに説明を反映（配列順と合わせる）
        for (int i = 0; i < levelUpTexts.Length; i++)
        {
            if (levelUpTexts[i] != null)
            {
                levelUpTexts[i].text = descriptions[i];
            }
            else
            {
                Debug.LogWarning($"levelUpTexts[{i}] が設定されていません");
            }
        }
    }
    public void OnAlpha1Button()
    {
        if (!GameManagement.Instance.tutorialLeveUpPlate) return;

        PlayerLevelUp(randomChoices[0]);
    }
    void PlayerLevelUp(int choice)
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
    string GetPlayerDescription(int choice)
    {
        switch (choice)
        {
            case 0: return "HP + 2";
            case 1: return "ダッシュクールタイム - 0.1秒";
            case 2: return "移動速度 + 1";
            default: return "";
        }
    }
    public void OnAlpha2Button()
    {
        if (!GameManagement.Instance.tutorialLeveUpPlate) return;

        MixLevelUp(randomChoices[2]);
    }
    void MixLevelUp(int choice)
    {
        string weaponID = Gun.Instance.GetCurrentWeaponID();
        StetusScript.Instance.level += 1;

        switch (weaponID)
        {
            case "handgun":
                 
                switch (choice)
                {

                    case 0:
                        StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
                        Debug.Log("HP　UP ");
                        break;
                    case 1:
                        StetusScript.Instance.PlayerDashCoolTime -= StetusScript.Instance.LevelUpDashCoolTime;
                        Debug.Log("回避クールタイム速度　UP ");
                        break;
                    case 2:
                        StetusScript.Instance.PlayerSpeed += StetusScript.Instance.LevelUpPlayerSpeed;
                        Debug.Log("移動速度　UP ");
                        break;
                    case 3:
                        gun.reloadTime = StetusScript.Instance.HandgunReloadTime -= 0.1f;
                        Debug.Log("リロード速度　UP hn");
                        break;
                    case 4:
                        gun.bulletLife = StetusScript.Instance.HandgunBalletLife += 1;
                        Debug.Log("敵の貫通する数　UP hn");
                        break;
                    case 5:
                        StetusScript.Instance.PlayerSpeed -= 0.3f;
                        Debug.Log("弾の大きさ　UP hn");
                        break;
                }

            break;

            case "ar":
                switch (choice)
                {
                    case 0:
                        StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
                        Debug.Log("HP　UP ");
                        break;
                    case 1:
                        StetusScript.Instance.PlayerDashCoolTime -= StetusScript.Instance.LevelUpDashCoolTime;
                        Debug.Log("回避クールタイム速度　UP ");
                        break;
                    case 2:
                        StetusScript.Instance.PlayerSpeed += StetusScript.Instance.LevelUpPlayerSpeed;
                        Debug.Log("移動速度　UP ");
                        break;
                    case 3:
                        gun.reloadTime = StetusScript.Instance.ArReloadTime -= 0.1f;
                        Debug.Log("リロード速度　UP");                        
                        break;
                    case 4:
                        gun.bulletLife = StetusScript.Instance.ArBalletLife += 1;
                        Debug.Log("敵の貫通する数　UP");
                         break;
                    case 5:
                        StetusScript.Instance.PlayerSpeed -= 0.3f;
                        Debug.Log("弾の大きさ　UP");
                        break;
                }

                break;

            case "sg":

                switch (choice)
                {
                    case 0:
                        StetusScript.Instance.PlayerHp += StetusScript.Instance.LevelUpPlayerHp;
                        Debug.Log("HP　UP ");
                        break;
                    case 1:
                        StetusScript.Instance.PlayerDashCoolTime -= StetusScript.Instance.LevelUpDashCoolTime;
                        Debug.Log("回避クールタイム速度　UP ");
                        break;
                    case 2:
                        StetusScript.Instance.PlayerSpeed += StetusScript.Instance.LevelUpPlayerSpeed;
                        Debug.Log("移動速度　UP ");
                        break;
                    case 3:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.reloadTime = StetusScript.Instance.SGReloadTime -= 0.3f;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }

                        Debug.Log("リロード速度　UP sg");
                        break;
                    case 4:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.bulletLife = StetusScript.Instance.SGBalletLife += 1;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }
                        Debug.Log("敵の貫通する数　UP sg");
                        break;
                    case 5:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.reloadTime = StetusScript.Instance.SGReloadTime -= 0.1f;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }
                        Debug.Log("弾の大きさ　UP sg");
                        break;

                }

                break;
        }

        RsumeScene();
    }
    string GetMixDescription(int choice)
    {
        string weaponID = Gun.Instance.GetCurrentWeaponID();

        switch (weaponID)
        {
            case "handgun":

                switch (choice)
                {
                    case 0: return "HP + 2";
                    case 1: return "ダッシュクールタイム - 0.1秒";
                    case 2: return "移動速度 + 1";
                    case 3: return "ハンドガンのリロード速度 - 0.3秒";
                    case 4: return "ハンドガンの貫通力 + 1";
                    case 5: return "ハンドガンの球の大きさ + 1";
                    default: return "";
                }

            case "ar":
                switch (choice)
                {
                    case 0: return "HP + 2";
                    case 1: return "ダッシュクールタイム - 0.1秒";
                    case 2: return "移動速度 + 1";
                    case 3: return "アサルトライフルのリロード速度 - 0.3秒";
                    case 4: return "アサルトライフルの貫通力 + 1";
                    case 5: return "アサルトライフルの球の大きさ + 1";
                    default: return "";
                }

            case "sg":
                switch (choice)
                {
                    case 0: return "HP + 2";
                    case 1: return "ダッシュクールタイム - 0.1秒";
                    case 2: return "移動速度 + 1";
                    case 3: return "ショットガンのリロード速度 - 0.3秒";
                    case 4: return "ショットガンの貫通力 + 1";
                    case 5: return "ショットガンの球の大きさ + 1";
                    default: return "";
                }

            default:
                return "";
        }
    }

    public void OnAlpha3Button()
    {
        if (!GameManagement.Instance.tutorialLeveUpPlate) return;

        GunLevelUp(randomChoices[1]);
    }
    void GunLevelUp(int choice)
    {
        string weaponID = Gun.Instance.GetCurrentWeaponID();
        StetusScript.Instance.level += 1;

        switch (weaponID)
        {
            case "handgun":


                switch (choice)
                {
                    case 3:
                        gun.reloadTime = StetusScript.Instance.HandgunReloadTime -= 0.1f;
                        Debug.Log("リロード速度　UP hn");
                        break;
                    case 4:
                        gun.bulletLife = StetusScript.Instance.HandgunBalletLife += 1;
                        Debug.Log("敵の貫通する数　UP hn");
                        break;
                    case 5:
                        StetusScript.Instance.PlayerSpeed -= 0.3f;
                        Debug.Log("弾の大きさ　UP hn");
                        break;

                }

                break;
            case "ar":
                switch (choice)
                {
                    case 3:
                        gun.reloadTime = StetusScript.Instance.ArReloadTime -= 0.1f;
                        Debug.Log("リロード速度　UP");
                        break;
                    case 4:
                        gun.bulletLife = StetusScript.Instance.ArBalletLife += 1;
                        Debug.Log("敵の貫通する数　UP");
                        break;
                    case 5:
                        StetusScript.Instance.PlayerSpeed -= 0.3f;
                        Debug.Log("弾の大きさ　UP");
                        break;

                }

                break;
            case "sg":

                switch (choice)
                {
                    case 3:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.reloadTime = StetusScript.Instance.SGReloadTime -= 0.3f;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }

                        Debug.Log("リロード速度　UP sg");
                        break;
                    case 4:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.bulletLife = StetusScript.Instance.SGBalletLife += 1;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }
                        Debug.Log("敵の貫通する数　UP sg");
                        break;
                    case 5:
                        if (Gun.Instance != null)
                        {
                            Gun.Instance.reloadTime = StetusScript.Instance.SGReloadTime -= 0.1f;
                        }
                        else
                        {
                            Debug.LogError("Gun.Instance が null です！");
                        }
                        Debug.Log("弾の大きさ　UP sg");
                        break;

                }

                break;
        }

        RsumeScene();
    }
    string GetGunDescription(int choice)
    {
        string weaponID = Gun.Instance.GetCurrentWeaponID();

        switch (weaponID)
        {
            case "handgun":

                switch (choice)
                {
                    case 3: return "ハンドガンのリロード速度 - 0.3秒";
                    case 4: return "ハンドガンの貫通力 + 1";
                    case 5: return "ハンドガンの球の大きさ + 1";
                    default: return "";
                }

            case "ar":
                switch (choice)
                {
                    
                    case 3: return "アサルトライフルのリロード速度 - 0.3秒";
                    case 4: return "アサルトライフルの貫通力 + 1";
                    case 5: return "アサルトライフルの球の大きさ + 1";
                    default: return "";
                }

            case "sg":
                switch (choice)
                {
                    
                    case 3: return "ショットガンのリロード速度 - 0.3秒";
                    case 4: return "ショットガンの貫通力 + 1";
                    case 5: return "ショットガンの球の大きさ + 1";
                    default: return "";
                }

            default:
                return "";
        }
    }
    

    public void RsumeScene()
    {
        //SceneTransitionManager.Instance.ReturnToStageScene();
        //Player.IsNotMove = false;

        GameManagement.Instance.isLevelUp = false;

        GameManagement.Instance.isPause = false;

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
