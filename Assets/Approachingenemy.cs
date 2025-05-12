using UnityEngine;

public class Approachingenemy : MonoBehaviour
{
    public float speed = 5f;            // 突っ込みスピード
    public int maxHP = 2;               // 最大HP
    private int currentHP;              // 現在のHP
    public GameObject itemPrefab;       // 倒したときに落とすアイテム

    private Vector2 moveDirection;      // 一度だけ決めた移動方向

    void Start()
    {
        Transform player = GameObject.FindWithTag("Player")?.transform;
        currentHP = maxHP;

        if (player != null)
        {
            moveDirection = (player.position - transform.position).normalized;
        }
        else
        {
            moveDirection = Vector2.zero; // プレイヤーが見つからない場合は動かない
        }
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
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
