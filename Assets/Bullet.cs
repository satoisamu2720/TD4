using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f; // 弾の寿命（秒数）
    public int damage = 1;      // ダメージ量

    private void Start()
    {
        // 指定時間後に自動で削除（保険）
        Destroy(gameObject, lifeTime);
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
            Approachingenemy enemy = other.GetComponent<Approachingenemy>();
            nWayBullet Boss = other.GetComponent<nWayBullet>();
            ShootEnemy shootEnemy = other.GetComponent<ShootEnemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (Boss != null)
            {
                Boss.TakeDamage(damage);
            }

            if (shootEnemy != null)
            {
                shootEnemy.TakeDamage(damage);
            }

            // 弾を消す（1ヒット制）
            Destroy(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
