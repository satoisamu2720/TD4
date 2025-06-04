using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private string ID;

    public GameObject weaponPrefab;

    // プレイヤーが武器を装備したかどうかのフラグ
    private bool isWeaponEquipped = false;

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

                // 武器を削除して消す
                Destroy(this.gameObject);

                // 武器を拾った後にチュートリアルを進める
                TutorialStepController.Instance.ProgressToNextStep();
            }
        }
    }
}
