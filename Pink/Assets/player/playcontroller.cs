using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playcontroller : MonoBehaviour
{
    private Rigidbody2D rg;
    private BoxCollider2D coll;
    private Animator animator;
    private AudioSource audioSource;
    private string currrntanimation = "";
    public float movespeed = 8f;

    float xVelocity;

    public float jumpForce = 6f;
    public float a = -0.65f;
    public float a1 = -0.4f;
    public float a2 = 0.4f;
    public int maxJumpCount = 0;
    private int jumpCount;

    // 状态
    public bool isOnGround;
    public bool isTouchingWall; // 表示是否碰撞到墙

    // 环境
    public LayerMask groundLayer;
    public float footOffset = 5f;
    public float headClearance = 0.5F;
    public float groundDistance = 0.2f;

    // 按键
    bool jumpPressed;

    // 用于保持角色前进的额外速度变量
    private float extraVelocity = 0f;

    // 记录开始反向移动的时间
    private float reverseStartTime = 0f;

    // 反向移动持续时间
    public float reverseDuration = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        checkanimation();
        jump();  
    }
    
    private void FixedUpdate() 
    {
        PhysicsCheck();
        Movement(); 
    }

    void PhysicsCheck()
    {
        Vector2 pos = transform.position;
        Vector2 offset = new Vector2(a1, -0.65f);
        Vector2 offset2 = new Vector2(a2, -0.65f);

        RaycastHit2D leftCheck = Physics2D.Raycast(pos + offset, Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Physics2D.Raycast(pos + offset2, Vector2.down, groundDistance, groundLayer);

        Debug.DrawRay(pos + offset, Vector2.down, Color.red, 0.1f);
        Debug.DrawRay(pos + offset2, Vector2.down, Color.red, 0.1f);

        if (leftCheck || rightCheck)
        {
            isOnGround = true;
            jumpCount = 0;
        }
        else 
        {
            isOnGround = false;
        }
    }

    void Movement()
    {
        xVelocity = Input.GetAxis("Horizontal");

        // 禁用在空中碰撞墙面时的移动
        /*if (isTouchingWall && !isOnGround)
        {
            // 保持原先方向的额外速度
            extraVelocity = xVelocity * 0.5f;
            xVelocity = 0;

            // 记录开始反向移动的时间
            reverseStartTime = Time.time;
        }
        else
        {
            // 如果开始反向移动后已经超过了反向移动持续时间，重置额外速度
            if (Time.time - reverseStartTime > reverseDuration)
            {
                extraVelocity = 0;
            }
        }*/

        rg.velocity = new Vector2((xVelocity * movespeed) + extraVelocity, rg.velocity.y);
        changeDirction();
    }

    void changeDirction()
    {
        if (xVelocity < 0)
        {
            transform.localScale = new Vector2(-4, 4);
        }
        if (xVelocity > 0)
        {
            transform.localScale = new Vector2(4, 4);
        }
    }

    void jump()
    {
        bool jumpPressed = Input.GetButtonDown("Jump");
        if (jumpPressed && (isOnGround || jumpCount < maxJumpCount))
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            jumpCount++;
            isOnGround = false;
            animator.SetBool("isJump", true);
            audioSource.Play();
        }
        else if (isOnGround)
        {
            animator.SetBool("isJump", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            isTouchingWall = true;
            // 记录开始反向移动的时间
            reverseStartTime = Time.time;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            isTouchingWall = false;
        }
    }

    public void checkanimation()
    {
        if (currrntanimation == "jump" && !isOnGround)
            return;

        if (xVelocity < 0 || xVelocity > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void IncreaseMaxJumpCount(int newMaxJumpCount)
    {
        maxJumpCount = newMaxJumpCount;
    }
}

