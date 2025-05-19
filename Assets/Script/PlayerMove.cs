using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static WeaponSpawn.WeaponCount;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public Vector2 lastMove;

    private Vector2 dashDirection;

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
    // 武器を取るかのフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Weapon();

        //Animate();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (isDash && collision.gameObject.tag == "block")
        //{
        //    StopCoroutine("Dash");
        //    rb.MovePosition(Vector2.zero);
        //    isDash = false;
        //    Debug.Log("壁でダッシュした。");
        //}


        if (collision.gameObject.tag == "Weapon")
        {
            //gameObject.SetActive(true);
            Debug.Log("触れている");

            // 全リストを取得
            List<string> myWeapons = WeaponLogger.GetAll();
            foreach (string weapon in myWeapons)
            {
                Debug.Log("所持中: " + weapon);
            }
        }

    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && !isDash)
        {
            StartCoroutine(Dash());
        }

        lastMove = movement;

    }

    private void Weapon()
    {
        if (WeaponLogger.Contains("gun"))
        {
            Debug.Log("その武器を持っています！");
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

}
