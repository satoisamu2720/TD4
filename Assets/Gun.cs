using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }
    public AudioClip SE;
    AudioSource audioSource;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireInterval = 0f;

    
    public float reloadTime = 2f;


    private int MaxAmmo = 0;
    private int currentAmmo;
    private float timer;
    private bool isReloading = false;

    [SerializeField]
    private TextMeshProUGUI ammoText;

    private Weapon currentWeaponData;
    private string currentWeaponID;

    void SetupWeaponByID(string id)
    {
        switch (id)
        {
            case "handgun":
                bulletSpeed = StetusScript.Instance.handgunBulletSpeed;
                fireInterval = StetusScript.Instance.handgunFireInterval;
                reloadTime = StetusScript.Instance.handgunReloadTime;
                MaxAmmo = StetusScript.Instance.handgunMaxAmmo;
                break;

            case "ar":
                bulletSpeed = StetusScript.Instance.ArBulletSpeed;
                fireInterval = StetusScript.Instance.ArFireInterval;
                reloadTime = StetusScript.Instance.ArReloadTime;
                MaxAmmo = StetusScript.Instance.ArMaxAmmo;
                break;

            default:
                bulletSpeed = StetusScript.Instance.handgunBulletSpeed;
                fireInterval = StetusScript.Instance.handgunFireInterval;
                reloadTime = StetusScript.Instance.handgunReloadTime;
                MaxAmmo = StetusScript.Instance.handgunMaxAmmo;
                break;
        }
    }

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        currentWeaponData = GetComponent<Weapon>();
        currentWeaponID = currentWeaponData != null ? currentWeaponData.GetID() : "default";
        SetupWeaponByID(currentWeaponID);
        PlayerMove playerMove = GetComponent<PlayerMove>();
        GetCurrentAmmo();
        // TextMeshProUGUI を再取得（タグや名前で探す）
        if (ammoText == null)
        {
            ammoText = GameObject.Find("AmmoText")?.GetComponent<TextMeshProUGUI>();
        }
        switch (currentWeaponID)
        {
            case "handgun":
                MaxAmmo = StetusScript.Instance.HandgunAmmo;
                break;
            case "ar":
                MaxAmmo = StetusScript.Instance.ArAmmo;
                break;
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

        int currentAmmo = GetCurrentAmmo();
        if (ammoText != null)
        {
            ammoText.text = "弾数: " + currentAmmo;
        }
        Weapon weaponComp = playerMove.GetComponentInChildren<Weapon>();
        if (weaponComp != null)
        {
            string newWeaponID = weaponComp.ID;
            if (newWeaponID != currentWeaponID)
            {
                currentWeaponID = newWeaponID;
                SetupWeaponByID(currentWeaponID);
                //currentAmmo = MaxAmmo; // 武器切り替えで弾数リセットしたい場合
            }
        }
        if (playerMove.isWeapon == true)
        {
           
            Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; 

            
            Vector3 direction = mousePos - transform.position;

            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            switch (currentWeaponID)
            {
                case "handgun":
                    bulletSpeed = StetusScript.Instance.handgunBulletSpeed;
                    fireInterval = StetusScript.Instance.handgunFireInterval;
                    reloadTime = StetusScript.Instance.handgunReloadTime;
                    MaxAmmo = StetusScript.Instance.handgunMaxAmmo;
                    break;

                case "ar":
                    bulletSpeed = StetusScript.Instance.ArBulletSpeed;
                    fireInterval = StetusScript.Instance.ArFireInterval;
                    reloadTime = StetusScript.Instance.ArReloadTime;
                    MaxAmmo = StetusScript.Instance.ArMaxAmmo;
                    break;

                default:
                    bulletSpeed = StetusScript.Instance.handgunBulletSpeed;
                    fireInterval = StetusScript.Instance.handgunFireInterval;
                    reloadTime = StetusScript.Instance.handgunReloadTime;
                    MaxAmmo = StetusScript.Instance.handgunMaxAmmo;
                    break;
            }

            // Rキーで手動リロード（残弾が満タンじゃないときだけ）
            if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < MaxAmmo)
            {
                StartCoroutine(Reload());
                return; 
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("残弾数: " + currentAmmo);
            }

            if (isReloading)
                return;

            timer += Time.deltaTime;


            switch (currentWeaponID)
            {
                case "handgun":
                    if (Input.GetMouseButtonDown(0) && timer >= fireInterval && currentAmmo > 0)
                    {
                        audioSource.PlayOneShot(SE);
                        Shoot();
                        timer = 0f;
                    }
                    break;

                case "ar":
                    if (Input.GetMouseButton(0) && timer >= fireInterval && currentAmmo > 0)
                    {
                        audioSource.PlayOneShot(SE);
                        Shoot();
                        timer = 0f;
                    }
                    break;

                default:
                    if (Input.GetMouseButtonDown(0) && timer >= fireInterval && currentAmmo > 0)
                    {
                        audioSource.PlayOneShot(SE);
                        Shoot();
                        timer = 0f;
                    }
                    break;
            }

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

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        int bulletLife = 1;
        int bulletDamage = 1;

        switch (currentWeaponID)
        {
            case "handgun":
                bulletDamage = StetusScript.Instance.handgunDamage;
                bulletLife = StetusScript.Instance.handgunBalletLife;
                StetusScript.Instance.HandgunAmmo--;
                break;
            case "ar":
                bulletDamage = 1;
                bulletLife = StetusScript.Instance.ArBalletLife;
                StetusScript.Instance.ArAmmo--;
                break;
        }
        bulletScript.Initialize(currentWeaponID, bulletLife, bulletDamage);
    }

    public int GetCurrentAmmo()
    {
        switch (currentWeaponID)
        {
            case "handgun": return StetusScript.Instance.HandgunAmmo;
            case "ar": return StetusScript.Instance.ArAmmo;
            default: return 0;
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        //Debug.Log("リロード中...");
        yield return new WaitForSeconds(reloadTime);
        switch (currentWeaponID)
        {
            case "handgun":
                StetusScript.Instance.HandgunAmmo = MaxAmmo;
                break;
            case "ar":
                StetusScript.Instance.ArAmmo = MaxAmmo;
                break;
        }
        isReloading = false;
       
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

    public void SetBgmVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp01(volume);
        }
    }

}