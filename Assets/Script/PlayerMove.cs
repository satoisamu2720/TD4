using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.UI;
//using static WeaponSpawn.WeaponCount;

public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    private GameObject mainWeapon;

    [SerializeField]
    private GameObject subWeapon;

    [SerializeField]
    private Transform weaponPos;

    private bool isMainWeapon;

    //移動速度
    [SerializeField]
    private float moveSpeed;
    // リギドボディ2D
    public Rigidbody2D rb;
    // 移動用変数
    private Vector2 movement;


    // ダッシュ機能フラグ
    [SerializeField]
    private bool isDash;
    // ダッシュスピード
    [SerializeField]
    private float dashSpeed;
    // ダッシュの再利用時間
    [SerializeField]
    private float dashDuration;
    // HP
    [SerializeField]
    private int HP;

    [SerializeField]
    private TextMeshProUGUI hpText;

    private GameObject currentWeapon;

    public bool isWeapon = false;

    // 無敵時間の長さ
    [SerializeField]
    private float invincibilityDuration = 2f;
    private bool isInvincible = false;
    // 無敵時間の残り時間
    private float invincibilityTimer = 0f;

    private SpriteRenderer spriteRenderer;
    //　元のカラー
    private Color originColor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Invincible();

        //Weapon();

        if (Input.GetKeyDown(KeyCode.Q)) // Qキーで切り替え
        {
            SwitchWeapon();
        }

        PlayerDirection();

        hpText.text = "PlayerHP: " + HP;

        //Animate();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Weapon")
        {
            //gameObject.SetActive(true);
            Debug.Log("触れている");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {

            if (!isInvincible)
            {
                HP--;
                StartInvincibility();
            }

            Debug.Log("当たった");
            //Destroy(collision.gameObject);
        }
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && !isDash)
        {
            StartCoroutine(Dash());
        }
    }

    public void WeaponObj(GameObject weaponObj)
    {
        GameObject weapon = Instantiate(weaponObj, weaponPos.position, Quaternion.identity, weaponPos);

        if (!isMainWeapon)
        {
            if (mainWeapon != null)
            {
                Destroy(mainWeapon);
            }
            mainWeapon = weapon;
            currentWeapon = mainWeapon;
        }
        else
        {
            if (subWeapon != null)
            {
                Destroy(subWeapon);
            }
            subWeapon = weapon;
        }

        isMainWeapon = !isMainWeapon; // 追加：次は逆のスロットに入れるよう切り替え
        ActivateCurrentWeapon();
    }

    //public void Animate()
    //{
    //    if (Mathf.Abs(movement.x) > 0.5f)
    //    {
    //        lastMove.x = movement.x;
    //        lastMove.y = 0;
    //    }
    //    if (Mathf.Abs(movement.y) > 0.5f)
    //    {
    //        lastMove.y = movement.y;
    //        lastMove.x = 0;
    //    }

    //    animator.SetFloat("Dir_X", movement.x);
    //    animator.SetFloat("Dir_Y", movement.y);
    //    animator.SetFloat("LastMove_X", lastMove.x);
    //    animator.SetFloat("LastMove_Y", lastMove.y);
    //    animator.SetFloat("Input", movement.magnitude);
    //}


    IEnumerator Dash()
    {
        isDash = true;

        rb.MovePosition(rb.position + movement * dashSpeed * Time.deltaTime);

        yield return new WaitForSeconds(dashDuration);

        isDash = false;

    }

    /// <summary>
    /// 無敵時間の開始
    /// </summary>
    private void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

    }
    /// <summary>
    /// 無敵時間の処理と点滅の処理
    /// </summary>
    private void Invincible()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            float alpha = Mathf.PingPong(Time.time * 10f, 1f);
            spriteRenderer.color = new Color(1f, 0f, 0f, alpha); // 赤点滅

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                spriteRenderer.color = originColor;
            }
        }
    }

    /// <summary>
    /// 武器の切り替え処理
    /// </summary>
    private void SwitchWeapon()
    {
        if (mainWeapon == null || subWeapon == null)
        {
            return;
        }

        if (currentWeapon == mainWeapon)
        {
            currentWeapon = subWeapon;
        }
        else
        {
            currentWeapon = mainWeapon;
        }

        ActivateCurrentWeapon();
    }

    /// <summary>
    /// 
    /// </summary>
    private void ActivateCurrentWeapon()
    {
        if (mainWeapon != null)
        {
            mainWeapon.SetActive(currentWeapon == mainWeapon);
            //isWeapon = true;
        }
        if (subWeapon != null)
        {
            subWeapon.SetActive(currentWeapon == subWeapon);
        }
    }

    /// <summary>
    /// マウスの位置にプレイヤーを向ける
    /// </summary>
    private void PlayerDirection()
    {
        // マウスのスクリーン座標を取得してワールド座標に変換
        Vector3 mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2DなのでZ座標は無視

        // プレイヤーの位置からマウスの位置への方向ベクトルを取得
        Vector3 direction = mousePosition - transform.position;

        // 角度を計算（atan2はラジアンで返すので、Degに変換）
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90f; // 必要に応じて調整

        // 回転をZ軸に対して適用
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}

