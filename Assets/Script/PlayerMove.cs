using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 lastMove;

    public LayerMask wallLayer;

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

        //Animate();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Weapon")
        {
            gameObject.SetActive(true);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDash && collision.gameObject.tag == "block")
        {
            StopCoroutine("Dash");
            rb.MovePosition(Vector2.zero);
            isDash = false;
            Debug.Log("壁でダッシュした。");
        }
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !isDash)
        {
            StartCoroutine(Dash());
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

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {

            // 壁が前にあるか確認（Raycast）
            RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDirection, 0.3f, wallLayer);
            if (hit.collider != null)
            {
                Debug.Log("壁にぶつかってダッシュ停止");
                break;
            }
        }

        rb.MovePosition(rb.position + movement * dashSpeed * Time.deltaTime);

        elapsed += Time.fixedDeltaTime;

        yield return new WaitForSeconds(dashDuration);

        isDash = false;

    }

}
