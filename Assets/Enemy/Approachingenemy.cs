using UnityEngine;
using UnityEngine.UI;

public class Approachingenemy : MonoBehaviour
{
    public float speed = 5f;
    public int maxHP = 2;
    public float rushDistance = 5f;
    public float waitTime = 1.5f;
    public GameObject itemPrefab;
    public GameObject arrowUIPrefab;

    public bool startImmediate = false; // ← miniEnemy なら true に
 
    private int currentHP;
    private Transform player;
    private Vector2 moveDirection;
    private Vector2 startPosition;
    private float waitTimer = 0f;

    private enum State { Idle, Rushing }
    private State state = State.Idle;
    private Vector2? injectedDirection = null;
    private Camera mainCamera;
    private RectTransform arrowInstance;

    [SerializeField] private float invincibilityDuration = 2f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private Color originColor;

    private float rushTimer = 0f;
    [SerializeField] private float rushTimeout = 1f;

    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentHP = maxHP;
        mainCamera = Camera.main;

        if (arrowUIPrefab != null)
        {
            GameObject arrowObj = Instantiate(arrowUIPrefab, GameObject.Find("Canvas").transform);
            arrowInstance = arrowObj.GetComponent<RectTransform>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;

       
        if (startImmediate)
        {
            // 注入されていれば使う／なければプレイヤー方向に突っ込む
            moveDirection = injectedDirection ?? (player != null ? (player.position - transform.position).normalized : Vector2.down);
            startPosition = transform.position;
            state = State.Rushing;
            rushTimer = rushTimeout;
        }
        else
        {
            waitTimer = waitTime;
        }
    }



    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
            if (player == null) return;
        }

        if (TextBoxController.IsTalking) return;
        if (PlayerMove.IsNotMove) return;

        if (GameManagement.Instance != null && GameManagement.Instance.isPause == false)
        {
            HandleStateMachine();
            HandleArrow();
            Invincible();
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
                    rushTimer = rushTimeout;
                }
                break;

            case State.Rushing:
                transform.Translate(moveDirection * speed * Time.deltaTime);
                float traveled = Vector2.Distance(startPosition, transform.position);
                rushTimer -= Time.deltaTime;

                if (traveled >= rushDistance || rushTimer <= 0f)
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

    public void TakeDamage(int damage)
    {
        if (isInvincible || isDead) return;

        StartInvincibility();
        currentHP -= damage;

        if (currentHP <= 0) Die();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }

        if (arrowInstance != null)
        {
            Destroy(arrowInstance.gameObject);
        }

        Destroy(gameObject);
    }

    private void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }

    private void Invincible()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            float alpha = Mathf.PingPong(Time.time * 10f, 1f);
            spriteRenderer.color = new Color(1f, 0f, 0f, alpha);

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                spriteRenderer.color = originColor;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == State.Rushing && collision.collider.CompareTag("Wall"))
        {
            state = State.Idle;
            waitTimer = waitTime;
        }
    }

    public void InitializeDirection(Vector2 dir)
    {
        injectedDirection = dir.normalized;
    }

}


