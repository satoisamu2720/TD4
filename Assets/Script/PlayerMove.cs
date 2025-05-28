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
    private Transform mainWeaponPos;

    //[SerializeField]
    //private GameObject subWeapon;

    [SerializeField]
    private Transform mainWeaponPos;

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

    private void Weapon()
    {
        if (WeaponLogger.Contains("gun"))
        {
            Debug.Log("その武器を持っています！");

        }
    }

    public void WeaponObj(GameObject weaponObj)
    {
        mainWeapon = Instantiate(weaponObj, mainWeaponPos.position, Quaternion.identity, mainWeaponPos);

        mainWeapon = Instantiate(weaponObj, mainWeaponPos.position, Quaternion.identity, mainWeaponPos);

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
