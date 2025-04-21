using UnityEngine;

public class Approachingenemy: MonoBehaviour
{
    public float speed = 5f;            // 突っ込みスピード
    public int maxHP = 2;               // 最大HP
    private int currentHP;              // 現在のHP
    public GameObject itemPrefab;       // 倒したときに落とすアイテム

    private Transform player;           // プレイヤーのTransform

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentHP = maxHP;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) // 弾に当たったら
        {
            TakeDamage(1);
            Destroy(collision.gameObject); // 弾を消す
        }
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
