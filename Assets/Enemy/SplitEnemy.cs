using UnityEngine;

public class SplitEnemy : MonoBehaviour
{
    public float speed = 5f;
    public float rushDistance = 5f;
    public float waitTime = 1.5f;
    public float rushTimeout = 1f;
    public float separationDistance = 1.5f; // 他の分裂体との最小距離

    private Transform player;
    private Vector2 moveDirection;
    private Vector2 startPosition;
    private float waitTimer = 0f;
    private float rushTimer = 0f;
    private enum State { Idle, Rushing }
    private State state = State.Idle;

    private Rigidbody2D rb;
    private GameObject[] allSplitEnemies;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        waitTimer = 0f; // 分裂直後から即行動
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
            return;
        }

        HandleSeparation(); // 他の分身と距離を保つ
        HandleStateMachine();
    }

    void HandleStateMachine()
    {
        switch (state)
        {
            case State.Idle:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    moveDirection = (player.position - transform.position).normalized;
                    startPosition = transform.position;
                    rushTimer = rushTimeout;
                    state = State.Rushing;
                }
                break;

            case State.Rushing:
                Vector2 newPos = rb.position + moveDirection * speed * Time.deltaTime;
                rb.MovePosition(newPos);

                float traveled = Vector2.Distance(startPosition, rb.position);
                rushTimer -= Time.deltaTime;

                if (traveled >= rushDistance || rushTimer <= 0f)
                {
                    state = State.Idle;
                    waitTimer = waitTime;
                }
                break;
        }
    }

    void HandleSeparation()
    {
        allSplitEnemies = GameObject.FindGameObjectsWithTag("MiniEnemy");
        foreach (var other in allSplitEnemies)
        {
            if (other == this.gameObject) continue;

            float dist = Vector2.Distance(transform.position, other.transform.position);
            if (dist < separationDistance)
            {
                // 近すぎるので逆方向に少し押し出す
                Vector2 pushDir = (transform.position - other.transform.position).normalized;
                rb.MovePosition(rb.position + pushDir * Time.deltaTime * 1f); // 押し出し速度
            }
        }
    }
}
