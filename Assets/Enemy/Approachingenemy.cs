using UnityEngine;
using UnityEngine.UI;

public class Approachingenemy : MonoBehaviour
{
    public float speed = 5f;
    public int maxHP = 2;
    public float rushDistance = 5f;
    public float waitTime = 1.5f;
    public GameObject itemPrefab;
    public GameObject arrowUIPrefab; // ← UI矢印プレハブをInspectorで割り当てる

    private int currentHP;
    private Transform player;
    private Vector2 moveDirection;
    private Vector2 startPosition;
    private float waitTimer = 0f;

    private enum State { Idle, Rushing }
    private State state = State.Idle;

    private Camera mainCamera;
    private RectTransform arrowInstance;

    private static Approachingenemy instance;
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentHP = maxHP;
        waitTimer = waitTime;
        mainCamera = Camera.main;

        // 矢印インスタンスを生成して非表示に
        if (arrowUIPrefab != null)
        {
            GameObject arrowObj = Instantiate(arrowUIPrefab, GameObject.Find("Canvas").transform);
            arrowInstance = arrowObj.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        HandleStateMachine();
        HandleArrow();
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

    void HandleArrow()
    {
        if (arrowInstance == null || mainCamera == null) return;

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        bool isOffScreen = viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f || viewportPos.z < 0f;

        arrowInstance.gameObject.SetActive(isOffScreen);

        if (isOffScreen)
        {
            // Viewport座標から方向ベクトルを計算
            Vector3 dir = (transform.position - mainCamera.transform.position).normalized;
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            Vector3 screenDir = new Vector3(dir.x, dir.y, 0).normalized;

            // 画面中心から方向へ150px移動した位置に矢印表示
            Vector3 screenPos = screenCenter + screenDir * 150f;
            screenPos.x = Mathf.Clamp(screenPos.x, 30f, Screen.width - 30f);
            screenPos.y = Mathf.Clamp(screenPos.y, 30f, Screen.height - 30f);

            arrowInstance.position = screenPos;

            // 矢印の回転を敵の方向に合わせる
            float angle = Mathf.Atan2(screenDir.y, screenDir.x) * Mathf.Rad2Deg;
            arrowInstance.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Bullet"))
    //    {
    //        TakeDamage(1);
    //        Destroy(collision.gameObject);
    //    }
    //}

    public void TakeDamage(int damage)
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

        if (arrowInstance != null)
        {
            Destroy(arrowInstance.gameObject);
        }

        Destroy(gameObject);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 2個目が生成されたら削除
        }
    }
}
