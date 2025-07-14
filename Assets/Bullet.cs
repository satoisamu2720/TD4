using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f; // 弾の寿命（秒数）
    public int damage = 1;      // ダメージ量
    public int life = 1;
    private string currentWeaponID;

    public void Initialize(string weaponID, int bulletLife, int bulletDamage)
    {
        currentWeaponID = weaponID;
        life = bulletLife;
        damage = bulletDamage;
    }
    private void Start()
    {
        // 指定時間後に自動で削除（保険）
        Destroy(gameObject, lifeTime);
        life = StetusScript.Instance.handgunBalletLife;
    }


    private void Update()
    {
        if (TextBoxController.IsTalking) return; // 会話中は入力無効
        if (Player.IsNotMove) return; // 会話中は入力無効
        // カメラのビューポート（0～1）外に出たら削除
        Vector3 screenPoint = UnityEngine.Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy タグのついたものに当たったらダメージ
        if (other.CompareTag("Enemy"))
        {
            bool hit = false;

            Approachingenemy enemy = other.GetComponent<Approachingenemy>();
            nWayBullet Boss = other.GetComponent<nWayBullet>();
            ShootEnemy shootEnemy = other.GetComponent<ShootEnemy>();
             

            if (enemy != null)
            {
                life--;
                hit = true;
                enemy.TakeDamage(damage);
            }

            if (Boss != null)
            {
                hit = true;
                Boss.TakeDamage(damage);
            }

            if (shootEnemy != null)
            {
                hit = true;
                shootEnemy.TakeDamage(damage);
            }

            if (hit)
            {
                life--;
                if (life <= 0)
                {
                    Destroy(gameObject);
                }
            }

        }
        

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
