using System;
using System.Linq;
using UnityEngine;
using static WeaponSpawn.WeaponCount;

public class StetusScript : MonoBehaviour
{
   
    public static StetusScript Instance { get; private set; }

    public PlayerExp playerExp;

    [Header("プレイヤーステータス")]
    public int PlayerHp = 5;
    public float PlayerSpeed = 1.0f;
    public float PlayerDashSpeed = 1.0f;
    public float PlayerDashCoolTime = 2.0f;

    //public int Exp;
    public int level = 1;

    [Header("ハンドガンステータス")]
    public int handgunDamage = 1;
    public int HandgunAmmo = 7;
    public int handgunMaxAmmo = 7;
    public float handgunBulletSpeed = 10.0f;
    public float handgunFireInterval = 0.8f;
    public float HandgunReloadTime = 1.0f;
    public int HandgunBalletLife = 1;
    public float HandgunSize = 0.5f;

    [Header("アサルトライフルステータス")]
    public int ArAmmo = 30;
    public int ArMaxAmmo = 30;
    public float ArBulletSpeed = 10.0f;
    public float ArFireInterval = 1.2f;
    public float ArReloadTime = 1.5f;
    public int ArBalletLife = 1;
    public float ArSize = 0.5f;


    [Header("ショットガンステータス")]
    public int SGAmmo = 5;
    public int SGMaxAmmo = 5;
    public float SGBulletSpeed = 10.0f;
    public float SGFireInterval = 1.2f;
    public float SGReloadTime = 1.5f;
    public int SGBalletLife = 1;
    public float SGSize = 0.5f;



    [System.Serializable]
    public class UserData
    {
        //プレイヤー
        public Vector3 position;
        public int playerLevel;
        public int health;
        public float speed;
        public float dashSpeed;
        public float dashCoolTime;

        //ハンドガン
        public int handgunAmmo;
        public int handgunBalletLife;
        public float handgunReloadTime;
        public float handgunSize;

        //アサルトライフル
        public int arAmmo;
        public int arBalletLife;
        public float arReloadTime;
        public float arSize;

        //ショットガン
        public int sgAmmo;
        public int sgBalletLife;
        public float sgReloadTime;
        public float sgSize;

        public int bullet;

        public bool mainWeapon;
        public string mainWeaponID;
        public string subWeaponID;

    }

    [Header("敵ステータス")]
    public int EnemyHp = 3;
    public float EnemySpeed = 5;
    public int TutorialBossEnemyHp = 0;
    public int Stage1BossEnemyHp = 0;
    public int Stage2BossEnemyHp = 0;



    [Header("レベルアップステータス")]
    public int LevelUpPlayerHp = 2;
    public float LevelUpDashCoolTime = 0.05f;
    public float LevelUpPlayerSpeed = 1.0f; 
    public float LevelUpBulletSize = 0.2f;

    void Start()
    {
        if (PlayerPrefs.HasKey("ShouldLoad") && PlayerPrefs.GetInt("ShouldLoad") == 1)
        {
            StetusScript.Instance.Load();
            PlayerPrefs.DeleteKey("ShouldLoad");
        }        
    }

    private void Update()
    {

       
    }
    public void Save()
    {
        
        Debug.Log("セーブされました");
        UserData data = new UserData()
        {
            //プレイヤー
            position = PlayerMove.Instance.transform.position,
            playerLevel = level,
            health = PlayerHp,
            speed = PlayerSpeed,
            dashSpeed = PlayerDashSpeed,
            dashCoolTime = PlayerDashCoolTime,

            //ハンドガン
            handgunAmmo = HandgunAmmo,
            handgunBalletLife = HandgunBalletLife,
            handgunReloadTime = HandgunReloadTime,
            handgunSize = HandgunSize,
            //アサルトライフル
            arAmmo = ArAmmo,
            arBalletLife = ArBalletLife,
            arReloadTime = ArReloadTime,
            arSize = ArSize,
            //ショットガン
            sgAmmo = SGAmmo,
            sgBalletLife = SGBalletLife,
            sgReloadTime = SGReloadTime,
            sgSize = SGSize,

            mainWeaponID = PlayerMove.Instance.GetWeaponID(true),
            subWeaponID = PlayerMove.Instance.GetWeaponID(false),
            mainWeapon = PlayerMove.Instance.isMainWeapon,
        };
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);

        PlayerPrefs.SetString("PlayerUserData", json);
        PlayerPrefs.Save();

        
    }

    public void Load()
    {
        Debug.Log("ロードされました");
        if (PlayerPrefs.HasKey("PlayerUserData"))
        {
            string json = PlayerPrefs.GetString("PlayerUserData");
            UserData data = JsonUtility.FromJson<UserData>(json);

            //プレイヤー
            PlayerMove.Instance.transform.position = data.position;
            level = data.playerLevel;
            PlayerHp = data.health;
            PlayerSpeed = data.speed;
            PlayerDashSpeed = data.dashSpeed;
            PlayerDashCoolTime = data.dashCoolTime;
            //ハンドガン
            HandgunAmmo = data.handgunAmmo;
            HandgunBalletLife = data.handgunBalletLife;
            HandgunReloadTime = data.handgunReloadTime;
            HandgunSize = data.handgunSize;
            //アサルトライフル
            ArAmmo = data.arAmmo;
            ArBalletLife = data.arBalletLife;
            ArReloadTime = data.arReloadTime;
            ArSize = data.arSize;
            //ショットガン
            SGAmmo = data.sgAmmo;
            SGBalletLife = data.sgBalletLife;
            SGReloadTime = data.sgReloadTime;
            SGSize = data.sgSize;

            
            PlayerMove.Instance.isMainWeapon = data.mainWeapon;

            // メイン武器ロード
            if (!string.IsNullOrEmpty(data.mainWeaponID))
            {
                GameObject mainPrefab = WeaponDatabase.Instance.GetWeaponPrefabByID(data.mainWeaponID);
                if (mainPrefab != null)
                {
                    mainPrefab.GetComponent<Weapon>().SetID(data.mainWeaponID);
                    PlayerMove.Instance.EquipWeaponAsSlot(mainPrefab, true); // true = main
                }
            }

            // サブ武器ロード
            if (!string.IsNullOrEmpty(data.subWeaponID))
            {
                GameObject subPrefab = WeaponDatabase.Instance.GetWeaponPrefabByID(data.subWeaponID);
                if (subPrefab != null)
                {
                    subPrefab.GetComponent<Weapon>().SetID(data.subWeaponID);
                    PlayerMove.Instance.EquipWeaponAsSlot(subPrefab, false); // false = sub
                }
            }

            Debug.Log(json);
        }
        else
        {
            Debug.Log("PlayerUserDataが存在しません");
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
    internal void ResetStatus()
    {
        throw new NotImplementedException();
    }
}
