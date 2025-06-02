using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireInterval = 0f;

    public int maxAmmo = 7;
    public float reloadTime = 2f;
    

    private int currentAmmo;
    private float timer;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }
    //
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletSpeed;

        currentAmmo--;
        Debug.Log("残弾数 : " + currentAmmo);
    }

    void Update()
    {
        // マウスのワールド座標を取得
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Zを0に固定（2D）

        // マウス方向のベクトル
        Vector3 direction = mousePos - transform.position;

        // マウス方向に銃を回転させる（スプライトが左向きの場合は +180）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);

        // Rキーで手動リロード（残弾が満タンじゃないときだけ）
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
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


    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("リロード中...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("リロード完了！残弾数: " + currentAmmo);
    }
}