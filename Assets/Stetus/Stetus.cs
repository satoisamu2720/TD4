using System;
using UnityEngine;

public class StetusScript : MonoBehaviour
{
    public static StetusScript Instance { get; private set; }

    public PlayerExp playerExp;

    [Header("プレイヤーステータス")]
    public int PlayerHp = 5;
    public float PlayerSpeed = 1.0f;
    
    //public int Exp;
    public int level = 1;

    [Header("ハンドガンステータス")]
    public int HandgunAmmo = 7;
    public int handgunMaxAmmo = 7;
    public float hundgunBulletSpeed = 10.0f;
    public float hundgunFireInterval = 0.8f;
    public float hundgunReloadTime = 1.0f;

    [Header("アサルトライフルステータス")]
    public int ArAmmo = 30;
    public int ArMaxAmmo = 7;
    public float ArBulletSpeed = 10.0f;
    public float ArFireInterval = 1.2f;
    public float ArReloadTime = 1.5f;
   


    [System.Serializable]
    public class UserData
    {
        public Vector3 position;
        public int health;
        public float speed;
        public int bullet;
        public int playerLevel;
        public bool mainWeapon;
        public string mainWeaponID;
        public string subWeaponID;
        public int handgunAmmo;
        public int arAmmo;
    }

    [Header("ボスステータス")]
    public int BossEnemyHp = 0;

    [Header("レベルアップステータス")]
    public int LevelUpPlayerHp = 2;
    public float LevelUpDashCoolTime = 0.05f;
    public int LevelUpGunMagazine = 1;

    void Start()
    {
        if (PlayerPrefs.HasKey("ShouldLoad") && PlayerPrefs.GetInt("ShouldLoad") == 1)
        {
            StetusScript.Instance.Load();
            PlayerPrefs.DeleteKey("ShouldLoad");
        }
        if (PlayerPrefs.HasKey("StartLoad") && PlayerPrefs.GetInt("StartLoad") == 1)
        {
            PlayerMove.Instance.transform.position = new Vector3(-150, -15, 0);
            PlayerPrefs.DeleteKey("StartLoad");
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("セーブされました");
            UserData data = new UserData()
            {
                position = PlayerMove.Instance.transform.position,
                health = PlayerHp,
                speed = PlayerSpeed,
                handgunAmmo = HandgunAmmo,
                arAmmo = ArAmmo,
                playerLevel = level,
                mainWeaponID = PlayerMove.Instance.GetWeaponID(true), 
                subWeaponID = PlayerMove.Instance.GetWeaponID(false),
                mainWeapon = PlayerMove.Instance.isMainWeapon,
            };
            string json = JsonUtility.ToJson(data, true);
            Debug.Log(json);

            PlayerPrefs.SetString("PlayerUserData", json);
            PlayerPrefs.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("ロードされました");
            if (PlayerPrefs.HasKey("PlayerUserData"))
            {
                string json = PlayerPrefs.GetString("PlayerUserData");
                UserData data = JsonUtility.FromJson<UserData>(json);

                PlayerMove.Instance.transform.position = data.position;
                PlayerHp = data.health;
                PlayerSpeed = data.speed;
                HandgunAmmo = data.handgunAmmo;
                ArAmmo = data.arAmmo;
                level = data.playerLevel;

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
                Debug.Log($"メイン: {PlayerMove.Instance.GetWeaponID(true)}, サブ: {PlayerMove.Instance.GetWeaponID(false)}");
            }
            else
            {
                Debug.Log("PlayerUserDataが存在しません");
            }
        }


    }
    public void Save()
    {

        Debug.Log("セーブされました");
        UserData data = new UserData()
        {
            position = PlayerMove.Instance.transform.position,
            health = PlayerHp,
            speed = PlayerSpeed,
            handgunAmmo = HandgunAmmo,
            arAmmo = ArAmmo,
            playerLevel = level,
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

            PlayerMove.Instance.transform.position = data.position;
            PlayerHp = data.health;
            PlayerSpeed = data.speed;
            HandgunAmmo = data.handgunAmmo;
            ArAmmo = data.arAmmo;
            level = data.playerLevel;

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
