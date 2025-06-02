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

    private GameObject currentWeapon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Weapon();

        if (Input.GetKeyDown(KeyCode.Q)) // Qキーで切り替え
        {
            SwitchWeapon();
        }

        Playerdirection();

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

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && !isDash)
        {
            StartCoroutine(Dash());
        }
    }

    //private void Weapon()
    //{
    //    if (WeaponLogger.Contains("gun"))
    //    {
    //        Debug.Log("その武器を持っています！");

    //    }
    //}

    public void WeaponObj(GameObject weaponObj)
    {
        GameObject weapon = Instantiate(weaponObj, weaponPos.position, Quaternion.identity, weaponPos);

        if (!isMainWeapon)
        {
            if (mainWeapon != null) Destroy(mainWeapon);
            mainWeapon = weapon;
            currentWeapon = mainWeapon;
        }
        else
        {
            if (subWeapon != null) Destroy(subWeapon);
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

    private void SwitchWeapon()
    {
        if (mainWeapon == null || subWeapon == null) return;

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

    private void ActivateCurrentWeapon()
    {
        if (mainWeapon != null) mainWeapon.SetActive(currentWeapon == mainWeapon);
        if (subWeapon != null) subWeapon.SetActive(currentWeapon == subWeapon);
    }

    private void Playerdirection()
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

