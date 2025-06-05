using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireInterval = 0f;

    
    public float reloadTime = 2f;

    private int currentAmmo;
    private float timer;
    private bool isReloading = false;

    private static Gun instance;

    [SerializeField]
    private TextMeshProUGUI ammoText;

    void Start()
    {
        currentAmmo = StetusScript.Instance.Bullet;

        PlayerMove playerMove = GetComponent<PlayerMove>();

        // TextMeshProUGUI を再取得（タグや名前で探す）
        if (ammoText == null)
        {
            ammoText = GameObject.Find("AmmoText")?.GetComponent<TextMeshProUGUI>();
        }

    }
    void Update()
    {
        if (TextBoxController.IsTalking) return; // 会話中は入力無効
        if (Player.IsNotMove) return; // 会話中は入力無効
        PlayerMove playerMove = GetComponent<PlayerMove>();

        if (ammoText == null)
        {
            GameObject ammoObj = GameObject.Find("AmmoText");
            if (ammoObj != null)
            {
                ammoText = ammoObj.GetComponent<TextMeshProUGUI>();
            }
        }

        //if (ammoText != null)
        //{
        //    ammoText.text = "弾数: " + currentAmmo;
        //}

        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("現在のフラグは: " + playerMove.isWeapon);
        }

        if (playerMove.isWeapon == true)
        {
            // マウスのワールド座標を取得
            Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // Zを0に固定（2D）

            // マウス方向のベクトル
            Vector3 direction = mousePos - transform.position;

            // マウス方向に銃を回転させる（スプライトが左向きの場合は +180）
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // Rキーで手動リロード（残弾が満タンじゃないときだけ）
            if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < StetusScript.Instance.Bullet)
            {
                StartCoroutine(Reload());
                return; // リロード中は発射させないために return
            }

            // リロード中は発射しない
            if (isReloading)
                return;

            timer += Time.deltaTime;

            // 左クリックかつ、間隔クリアかつ弾ありで発射
            if (Input.GetMouseButtonDown(0) && timer >= fireInterval && currentAmmo > 0)
            {
                Shoot();
                timer = 0f;
            }

            // 弾切れでリロード開始
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }


    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletSpeed;

        currentAmmo--;
        Debug.Log("残弾数 : " + currentAmmo);
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("リロード中...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = StetusScript.Instance.Bullet;
        isReloading = false;
        Debug.Log("リロード完了！残弾数: " + currentAmmo);
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
            Destroy(gameObject); // 2個目を防ぐ
        }
    }

}