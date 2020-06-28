using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    [Header("跳跃参数")]
    public float speed;
    public float jumpForce;
    private float horizontalMove;
    public Transform groundCheck;
    public LayerMask ground;

    [Header("Dash参数")]
    public float dashTime;      //dash时长
    private float dashTimeLeft; //dash剩余时长
    private float lastDash = -10f;     //上一次dash时间点
    public float dashCD;        //dash CD
    public float dashSpeed;     //dash 速度


    [Header("Player是否处于某状态中")]
    public bool isGround;
    public bool isJump;
    public bool isDashing;

    bool jumpPressed;
    int jumpCount;

    [Header("UI")]
    public Image CDImage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 跳跃
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        // Dash
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(Time.time>=(lastDash+dashCD))
            {
                ReadyToDash();
            }
        }

        // UI
        CDImage.fillAmount -= 1.0f / dashCD * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // 检测是否处于地面上
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        // Input Dash
        Dash();
        if (isDashing)
            return;

        // Input移动控制
        GroundMovement();

        // Input跳跃
        Jump();

        // 动画状态切换
        SwitchAnim();
    }

    /// <summary>
    /// Input移动控制
    /// </summary>
    void GroundMovement()
    {
        // 改变player的速度
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        // 改变player的方向
        if (horizontalMove != 0)
            transform.localScale = new Vector3(horizontalMove, 1, 1);
    }

    /// <summary>
    /// Input跳跃
    /// </summary>
    void Jump()
    {
        // 如果处于地面，则拥有2次跳跃次数
        if (isGround)
        {
            jumpCount = 2;//可跳跃数量
            isJump = false;
        }

        // 如果处于地面，并按下Jump键，则进行跳跃
        // 若处于空中，并按下Jump键，则进行二次跳跃
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;

            // 注：跳跃完重置该参数
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    private void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        CDImage.fillAmount = 1.0f;
    }

    /// <summary>
    /// 冲刺
    /// </summary>
    private void Dash()
    {
        if(isDashing)
        {
            if(dashTimeLeft>0)
            {
                // 起跳时dash
                if(rb.velocity.y>0 && !isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * horizontalMove, jumpForce);
                }

                rb.velocity = new Vector2(dashSpeed * horizontalMove, rb.velocity.y);

                dashTimeLeft -= Time.deltaTime;

                ShadowPool.instance.GetFromPool();
            }
            else
            {
                // dash完
                isDashing = false;
                if (!isGround)
                {
                    //rb.velocity = new Vector2(dashSpeed * horizontalMove, jumpForce);
                }
            }
        }
    }

    /// <summary>
    /// 动画切换控制
    /// </summary>
    void SwitchAnim()
    {
        // 设置奔跑数值，范围 [0, 1*speed]
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            // 当处于空中，且player有 +Y速度，说明在起跳阶段
            anim.SetBool("jumping", true);
        }
        else if (!isGround && rb.velocity.y < 0)
        {
            // 当player有 -Y速度，说明在下落阶段
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }
}