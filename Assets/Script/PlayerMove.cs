using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 lastMove;

    //移動速度
    [SerializeField]
    private float moveSpeed;
    // リギドボディ2D
    private Rigidbody2D rb;
    // 移動用変数
    private Vector2 posMove;

    // ダッシュ機能フラグ
    [SerializeField]
    private bool isDash;
    // ダッシュスピード
    [SerializeField]
    private float dashSpeed;
    // ダッシュの再利用時間
    [SerializeField]
    private float dashDuration;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        posMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
        //Animate();

      

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!isDash)
        {
            rb.MovePosition(rb.position + posMove * moveSpeed);
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

        rb.MovePosition(rb.position + posMove* dashSpeed);

        yield return new WaitForSeconds(dashDuration);

        isDash = false;

    }

}
