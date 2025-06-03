using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

        PlayerMove playerMove = GetComponent<PlayerMove>();

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

        PlayerMove playerMove = GetComponent<PlayerMove>();

        if (Input.GetKey(KeyCode.T)) {
            Debug.Log("現在のフラグは: " + playerMove.isWeapon);
        }
    


        if(playerMove.isWeapon == true)
        {
           
            Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; 

            
            Vector3 direction = mousePos - transform.position;

            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            
            if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
            {
                StartCoroutine(Reload());
                return; 
            }

           
            if (isReloading)
                return;

            timer += Time.deltaTime;

            
            if (Input.GetMouseButtonDown(0) && timer >= fireInterval && currentAmmo > 0)
            {
                Shoot();
                timer = 0f;
            }

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
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