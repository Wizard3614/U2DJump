using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playcontroller : MonoBehaviour
{
    private Rigidbody2D rg;
    private BoxCollider2D coll;
    private Animator animator;//获取animation
    private string currrntanimation="";//获取当前进行的动画，以便进行下一个动画是否进行的判断
    public float movespeed = 8f;//移动速度乘积。

    float xVelocity;//水平移动力

    public float jumpForce = 6f;//跳跃方向力


    //状态
    public bool isjump = false;
    public bool isOnGround;

    //环境
    public LayerMask groundLayer;
    public float footOffset = 5f;
    public float headClearance = 0.5F;
    public float groundDistance = 0.2f;

    //按键
    bool jumpPressed;

    // Start is called before the first frame update
    void Start()
    {
        rg=GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
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

    void PhysicsCheck()//射线来判断人物的状态
    {
        Vector2 pos = transform.position;
        Vector2 offset = new Vector2(-0.4f,-0.65f);
        Vector2 offset2 = new Vector2(0.4f,-0.65f);


        RaycastHit2D  leftCheck = Physics2D.Raycast(pos+offset,Vector2.down,groundDistance,groundLayer);
        RaycastHit2D  rightCheck = Physics2D.Raycast(pos+offset2,Vector2.down,groundDistance,groundLayer);

        Debug.DrawRay(pos+offset,Vector2.down,Color.red,0.1f);
        Debug.DrawRay(pos+offset2,Vector2.down,Color.red,0.1f);//测试。

        if(leftCheck||rightCheck)
        {
            isOnGround = true;
            isjump= false;
        }
        else 
        {
            isOnGround = false;
            isjump=true;
        }
    }
    void  Movement()//角色移动
    {

        
        xVelocity = Input.GetAxis("Horizontal");
        rg.velocity = new Vector2(xVelocity*movespeed,rg.velocity.y);
        changeDirction();
    }

    void changeDirction()//角色移动方向翻转
    {
        if(xVelocity<0)
        {
            transform.localScale = new Vector2(-4,4);
            
        }
        if(xVelocity>0)
        {
            transform.localScale = new Vector2(4,4);
            
        }
    }

    void jump()
    {
        jumpPressed = Input.GetButtonDown("Jump");//单次跳跃
        if(jumpPressed&&isOnGround)
        {
            
            isOnGround = false;
            isjump =true;
            rg.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
        }

        else if (isOnGround)
        {
            isjump = false;
        }
        animator.SetBool("isJump",isjump);
    }

    // public void animationchange (string animation , float time = 0.2f, float t=0)//选择进行的动画和动画过渡时间
    // {
    //     if(t>0)
    //     {
    //         StartCoroutine(Wait());
    //
    //     }
    //
    //     else  Validate();
    //
    //     IEnumerator Wait()
    //     {
    //         yield return  new WaitForSeconds(t-time);
    //         Validate();
    //     }
    //
    //     void Validate()
    //     {
    //         if(currrntanimation != animation)
    //     {
    //         currrntanimation = animation;
    //         if(currrntanimation =="")
    //         checkanimation();
    //         else 
    //         animator.CrossFade(animation,time);//动画过度，两个变量为目标动画的名字和过渡时间；
    //     }
    //     }
    //     
    //     
    // }

    public void checkanimation()
    {
        if(currrntanimation == "jump"&&!isOnGround)//避免其他动画打断跳跃动画
        return;

        if (xVelocity  < 0 || xVelocity > 0)
        {
            animator.SetBool("isRunning",true);
        }
        else
        {
            animator.SetBool("isRunning",false);
        }
    }
}
