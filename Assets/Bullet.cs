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
        // カメラのビューポート（0～1）外に出たら削除
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
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
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 弾を消す（1ヒット制）
            Destroy(gameObject);
        }
    }
}
