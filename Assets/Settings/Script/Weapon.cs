using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{

    public static Weapon Instance { get; private set; }

    [SerializeField]
    public string ID;

    public GameObject weaponPrefab;

    // プレイヤーが武器を装備したかどうかのフラグ
    private bool isWeaponEquipped = false;

    public static event Action OnWeaponPickedUp;


    private void Start()
    {
        PlayerMove playerMove = GetComponent<PlayerMove>();
    }

    public void SetID(string id)
    {
        ID = id;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Fキーを押すことで武器を装備する
            if (Input.GetKey(KeyCode.F) && !isWeaponEquipped)
            {
                // 武器のIDをログに追加
                WeaponLogger.Add(ID);

                // プレイヤーが武器を装備
                PlayerMove playerMove = other.GetComponent<PlayerMove>();
                playerMove.WeaponObj(this.gameObject);

                // 武器が装備されたことを記録
                isWeaponEquipped = true;

                Debug.Log("武器が装備されました");

                OnWeaponPickedUp?.Invoke();
                // 武器を削除して消す
                Destroy(this.gameObject);
            }
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

    public string GetID()
    {
        return ID;
    }
}
