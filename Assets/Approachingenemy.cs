using UnityEngine;

public class Approachingenemy : MonoBehaviour
{
    public float speed = 5f;
    public int maxHP = 2;
    public float rushDistance = 3f;       // 1âÒÇÃìÀêiãóó£
    public float waitTime = 1.5f;         // ë“ã@éûä‘
    public GameObject itemPrefab;

    private int currentHP;
    private Transform player;
    private Vector2 moveDirection;
    private Vector2 startPosition;
    private float waitTimer = 0f;

    private enum State { Idle, Rushing }
    private State state = State.Idle;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentHP = maxHP;
        waitTimer = waitTime; // ç≈èâÇ…è≠Çµé~Ç‹ÇÈ
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f && player != null)
                {
                    moveDirection = (player.position - transform.position).normalized;
                    startPosition = transform.position;
                    state = State.Rushing;
                }
                break;

            case State.Rushing:
                transform.Translate(moveDirection * speed * Time.deltaTime);
                float traveled = Vector2.Distance(startPosition, transform.position);
                if (traveled >= rushDistance)
                {
                    state = State.Idle;
                    waitTimer = waitTime;
                }
                break;
        }
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
