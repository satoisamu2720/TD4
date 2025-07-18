using UnityEngine;
using UnityEngine.UI;

public class DivisionEnemy : MonoBehaviour
{

    public static DivisionEnemy Instance { get; private set; }
    public float speed = 5f;
    public float rushDistance = 5f;
    public float waitTime = 1.5f;
    public GameObject itemPrefab;
    public GameObject arrowUIPrefab;

    public GameObject miniEnemyPrefab;
    public int numberOfSplits = 2;
    public float splitSpreadAngle = 90f;

    public int currentHP;
    private Transform player;
    private Vector2 moveDirection;
    private Vector2 startPosition;
    private float waitTimer = 0f;

    private enum State { Idle, Rushing }
    private State state = State.Idle;

    private Camera mainCamera;
    private RectTransform arrowInstance;

    [SerializeField]
    private float invincibilityDuration = 2f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private Color originColor;

    private Rigidbody2D rb; // Rigidbody2D追加

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentHP = StetusScript.Instance.Stage2BossEnemyHp;
        waitTimer = waitTime;
        mainCamera = Camera.main;

        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D取得

        if (arrowUIPrefab != null)
        {
            GameObject arrowObj = Instantiate(arrowUIPrefab, GameObject.Find("Canvas").transform);
            arrowInstance = arrowObj.GetComponent<RectTransform>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;

        // ミニ敵用：プレイヤーがいれば最初からラッシュ状態にして追尾開始
        if (player != null && state == State.Idle && gameObject.CompareTag("MiniEnemy"))
        {
            moveDirection = (player.position - transform.position).normalized;
            startPosition = transform.position;
            state = State.Rushing;
        }
    }

    void Update()
    {
        if (GameManagement.Instance != null && GameManagement.Instance.isPause == false)
        {

            HandleStateMachine();
            HandleArrow();
            HandleInvincibility();
        }
    }

    void HandleStateMachine()
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
                if (player != null)
                {
                    // 毎フレームプレイヤーに追従
                    moveDirection = (player.position - transform.position).normalized;
                }
                Vector2 newPos = rb.position + moveDirection * speed * Time.deltaTime;
                rb.MovePosition(newPos);

                float traveled = Vector2.Distance(startPosition, rb.position);
                if (traveled >= rushDistance)
                {
                    state = State.Idle;
                    waitTimer = waitTime;
                }
                break;
        }
    }

    void HandleArrow()
    {
        if (arrowInstance == null || mainCamera == null) return;

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        bool isOffScreen = viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f || viewportPos.z < 0f;

        arrowInstance.gameObject.SetActive(isOffScreen);

        if (isOffScreen)
        {
            Vector3 dir = (transform.position - mainCamera.transform.position).normalized;
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            Vector3 screenDir = new Vector3(dir.x, dir.y, 0).normalized;

            Vector3 screenPos = screenCenter + screenDir * 150f;
            screenPos.x = Mathf.Clamp(screenPos.x, 30f, Screen.width - 30f);
            screenPos.y = Mathf.Clamp(screenPos.y, 30f, Screen.height - 30f);

            arrowInstance.position = screenPos;

            float angle = Mathf.Atan2(screenDir.y, screenDir.x) * Mathf.Rad2Deg;
            arrowInstance.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (!isInvincible)
            {
                //TakeDamage(1);
                //StartInvincibility();
            }

            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartInvincibility();
        }
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }

        if (arrowInstance != null)
        {
            Destroy(arrowInstance.gameObject);
        }

        // すでにミニ敵だったら分裂しない
        if (this.CompareTag("MiniEnemy") == false)
        {
            Split();
        }

        Destroy(gameObject);
    }


    void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }

    void HandleInvincibility()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            float alpha = Mathf.PingPong(Time.time * 10f, 0.5f) + 0.5f; // 透明度 0.5～1.0
            spriteRenderer.color = new Color(1f, 0f, 0f, alpha); // 赤く点滅

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                spriteRenderer.color = originColor;
            }
        }
    }

    void Split()
    {
        if (miniEnemyPrefab == null || numberOfSplits <= 0) return;

        float angleStep = splitSpreadAngle / (numberOfSplits - 1);
        float startAngle = -splitSpreadAngle / 2;

        for (int i = 0; i < numberOfSplits; i++)
        {
            GameObject mini = Instantiate(miniEnemyPrefab, transform.position, Quaternion.identity);
            mini.tag = "MiniEnemy"; // 必須: 分離距離処理の対象にする
            float angle = startAngle + angleStep * i;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

            Rigidbody2D miniRB = mini.GetComponent<Rigidbody2D>();
            if (miniRB != null)
            {
                miniRB.AddForce(direction * 3f, ForceMode2D.Impulse);
            }
        }
    }






    // ミニ敵用の初期化メソッドを追加
    public void InitializeMiniEnemy(Transform targetPlayer, Vector2 initialDirection)
    {
        player = targetPlayer;
        currentHP = StetusScript.Instance.Stage2BossEnemyHp;
        moveDirection = initialDirection.normalized;
        startPosition = transform.position;
        state = State.Rushing;
        waitTimer = 0f;
    }

    // 壁にぶつかったらIdleに戻す
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == State.Rushing && collision.collider.CompareTag("Wall"))
        {
            state = State.Idle;
            waitTimer = waitTime;
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
}
