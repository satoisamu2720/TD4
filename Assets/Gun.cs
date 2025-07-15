using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }

    [Header("ハンドガンSE")]
    public AudioClip HSE;
    public AudioClip HRSE;
    AudioSource handgunSE;
    AudioSource handgunReloadSE;

    [Header("アサルトライフルSE")]
    public AudioClip ARSE;
    public AudioClip ARRSE;
    AudioSource arSE;
    AudioSource arReloadSE;

    [Header("ショットガンSE")]
    public AudioClip SGSE;
    public AudioClip SGRSE;
    AudioSource sgSE;
    AudioSource sgReloadSE;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 0;
    public float fireInterval = 0f;

    
    public float reloadTime = 2f;


    private int MaxAmmo = 0;
    private int currentAmmo;
    private float timer;
    private bool isReloading = false;

    public ReloadUI reloadUI;

    [SerializeField]
    private TextMeshProUGUI ammoText;

    private Weapon currentWeaponData;
    private string currentWeaponID;

    int bulletLife = 1;
    int bulletDamage = 1;
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
            case "sg":
                bulletSpeed = StetusScript.Instance.SGBulletSpeed;
                fireInterval = StetusScript.Instance.SGFireInterval;
                reloadTime = StetusScript.Instance.SGReloadTime;
                MaxAmmo = StetusScript.Instance.SGMaxAmmo;
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

        handgunSE = GetComponent<AudioSource>();
        handgunReloadSE = GetComponent<AudioSource>();

        arSE = GetComponent<AudioSource>();
        arReloadSE = GetComponent<AudioSource>();

        sgSE = GetComponent<AudioSource>();
        sgReloadSE = GetComponent<AudioSource>();

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
            case "sg":
                MaxAmmo = StetusScript.Instance.SGAmmo;
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
                    //MaxAmmo = StetusScript.Instance.handgunMaxAmmo;
                    break;

                case "ar":
                    bulletSpeed = StetusScript.Instance.ArBulletSpeed;
                    fireInterval = StetusScript.Instance.ArFireInterval;
                    reloadTime = StetusScript.Instance.ArReloadTime;
                    //MaxAmmo = StetusScript.Instance.ArMaxAmmo;
                    break;

                case "sg":
                    bulletSpeed = StetusScript.Instance.SGBulletSpeed;
                    fireInterval = StetusScript.Instance.SGFireInterval;
                    reloadTime = StetusScript.Instance.SGReloadTime;
                    //MaxAmmo = StetusScript.Instance.SGAmmo;
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
                Debug.Log("残弾数: " + MaxAmmo);
            }

            if (isReloading)
                return;

            timer += Time.deltaTime;


            switch (currentWeaponID)
            {
                case "handgun":
                    if (Input.GetMouseButtonDown(0) && timer >= fireInterval && currentAmmo > 0)
                    {
                        handgunSE.PlayOneShot(HSE);
                        Shoot();
                        timer = 0f;
                    }
                    break;

                case "ar":
                    if (Input.GetMouseButton(0) && timer >= fireInterval && currentAmmo > 0)
                    {
                        arSE.PlayOneShot(ARSE);
                        Shoot();
                        timer = 0f;
                    }
                    break;
                case "sg":
                    if (Input.GetMouseButton(0) && timer >= fireInterval && currentAmmo > 0)
                    {
                        sgSE.PlayOneShot(SGSE);
                        Shoot();
                        timer = 0f;
                    }
                    break;

                default:
                    if (Input.GetMouseButtonDown(0) && timer >= fireInterval && currentAmmo > 0)
                    {
                        handgunSE.PlayOneShot(HSE);
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
            case "sg":
                FireNWaysShotgun();
                StetusScript.Instance.SGAmmo--;
                break;
        }
        bulletScript.Initialize(currentWeaponID, bulletLife, bulletDamage);
       
    }
    void FireNWaysShotgun()
    {
        int bulletCount = 5;             // 弾の数
        float spreadAngle = 30f;         // 扇の角度（度）
        float baseAngle = firePoint.rotation.eulerAngles.z;
        float startAngle = baseAngle - spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (spreadAngle / (bulletCount - 1)) * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = dir * bulletSpeed;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            int bulletLife = StetusScript.Instance.SGAmmo; // SGの設定使う
            int bulletDamage = 1;
            bulletScript.Initialize(currentWeaponID, bulletLife, bulletDamage);
        }
    }
    public int GetCurrentAmmo()
    {
        switch (currentWeaponID)
        {
            case "handgun": return StetusScript.Instance.HandgunAmmo;
            case "ar": return StetusScript.Instance.ArAmmo;
            case "sg": return StetusScript.Instance.SGAmmo;
            default: return 0;
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        //Debug.Log("リロード中...");
        switch (currentWeaponID)
        {
            case "handgun":
                handgunReloadSE.PlayOneShot(HRSE);
                break;
            case "ar":
                arReloadSE.PlayOneShot(ARRSE);
                break;
            case "sg":
                sgReloadSE.clip = SGRSE;        
                sgReloadSE.loop = true;          
                sgReloadSE.Play();
                break;
        }
        if (reloadUI != null)
        {
            reloadUI.StartReload(reloadTime);
        }

        yield return new WaitForSeconds(reloadTime);
        switch (currentWeaponID)
        {
            case "handgun":
                StetusScript.Instance.HandgunAmmo = MaxAmmo;
                break;
            case "ar":
                StetusScript.Instance.ArAmmo = MaxAmmo;
                break;
            case "sg":
                StetusScript.Instance.SGAmmo = MaxAmmo;
                sgReloadSE.Stop();         
                sgReloadSE.loop = false;
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
        if (handgunSE != null)
        {
            handgunSE.volume = Mathf.Clamp01(volume);
        }
        if (handgunReloadSE != null)
        {
            handgunReloadSE.volume = Mathf.Clamp01(volume);
        }
        if (arSE != null)
        {
            arSE.volume = Mathf.Clamp01(volume);
        }
        if (arReloadSE != null)
        {
            arReloadSE.volume = Mathf.Clamp01(volume);
        }
        if (sgSE != null)
        {
            sgSE.volume = Mathf.Clamp01(volume);
        }
        if (sgReloadSE != null)
        {
            sgReloadSE.volume = Mathf.Clamp01(volume);
        }
    }

}