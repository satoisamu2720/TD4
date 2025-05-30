using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static WeaponSpawn.WeaponCount;

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
        if (!isMainWeapon) {
            mainWeapon = Instantiate(weaponObj, weaponPos.position, Quaternion.identity, weaponPos);
        }

        if (isMainWeapon)
        {
            subWeapon = Instantiate(weaponObj, weaponPos.position, Quaternion.identity, weaponPos);
        }


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

}
