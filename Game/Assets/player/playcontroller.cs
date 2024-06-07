using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool dashPressed;
    bool slidePressed;

    // 冲刺参数
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private float dashTime;

    // 滑行参数
    public float slideSpeed = 12f;
    public float slideDuration = 0.5f;
    private bool isSliding = false;
    private float slideTime;

    // 用于保持角色前进的额外速度变量
    private float extraVelocity = 0f;

    // 记录开始反向移动的时间
    private float reverseStartTime = 0f;

    // 反向移动持续时间
    public float reverseDuration = 0.4f;

    // 保存位置
    private Vector2 savedPosition;
    private GameObject savedObject;
    public GameObject markerPrefab; // 用于保存标记的预制体

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // 订阅场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 取消订阅场景加载事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 在新场景加载时调用此方法
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (savedObject != null)
        {
            Destroy(savedObject);
            savedObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkanimation();
        jump();

        // 检测冲刺按键
        if (Input.GetMouseButtonDown(1) && dashTime <= 0) // 检测鼠标右键
        {
            dashPressed = true;
        }

        // 检测滑行按键
        if (Input.GetKeyDown(KeyCode.R) && isOnGround && !isSliding)
        {
            isSliding = true;
            slideTime = slideDuration;
            animator.SetBool("isSliding", true); // 设置滑行动画
        }

        // 检测保存位置按键
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveOrTeleportPosition();
        }
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

        if (dashPressed && dashTime <= 0)
        {
            dashTime = dashDuration;
            dashPressed = false;
        }

        if (dashTime > 0)
        {
            float dashDirection = transform.localScale.x > 0 ? 1 : -1;
            rg.velocity = new Vector2(dashDirection * dashSpeed, rg.velocity.y);
            dashTime -= Time.fixedDeltaTime;
        }
        else if (isSliding)
        {
            float slideDirection = transform.localScale.x > 0 ? 1 : -1;
            rg.velocity = new Vector2(slideDirection * slideSpeed, rg.velocity.y);
            slideTime -= Time.fixedDeltaTime;

            if (slideTime <= 0)
            {
                isSliding = false;
                animator.SetBool("isSliding", false); // 结束滑行动画
            }
        }
        else
        {
            rg.velocity = new Vector2((xVelocity * movespeed) + extraVelocity, rg.velocity.y);
        }

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

    // 保存或传送位置
    void SaveOrTeleportPosition()
    {
        if (savedObject == null)
        {
            savedPosition = transform.position;
            savedObject = Instantiate(markerPrefab, savedPosition, Quaternion.identity);
        }
        else
        {
            StartCoroutine(TeleportAndRemoveMarker());
        }
    }

    IEnumerator TeleportAndRemoveMarker()
    {
        // 等待一小段时间，确保标记物体不会立即消失
        yield return new WaitForSeconds(0.1f);
        transform.position = savedPosition;
        Destroy(savedObject);
        savedObject = null;
    }
}
