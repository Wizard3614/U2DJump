using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    #region Singleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;
    
    private bool invincible = false; // 是否处于无敌状态
    private float invincibleDuration = 1f; // 无敌持续时间
    private float invincibleTimer = 0f; // 无敌计时器
    
    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (invincible)
        {

            invincibleTimer += Time.deltaTime; // 计时器累加
            if (invincibleTimer >= invincibleDuration)
            {
                invincible = false; // 取消无敌状态
                animator.SetBool("isHitten",false);
            }
            else{
                float remainder = invincibleTimer % 0.2f;
                spriteRenderer.enabled = remainder > 0.10f;
            }
        }
    }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        
        if (!invincible) // 如果不是无敌状态
        {
            health -= dmg;
            ClampHealth();
            if (health <= 0)
            {
                animator.SetBool("isDead",true);
                GetComponent<Playcontroller>().enabled = false;
                StartCoroutine(HandleHealth());
                ;
            }
            else
            {
                invincible = true; // 设置为无敌状态
                invincibleTimer = 0f; // 重置计时器
                animator.SetBool("isHitten",true);
            }
        }
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }   
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
    private IEnumerator HandleHealth()
    {
        // 等待死亡动画播放完成，假设动画长度为2秒
        yield return new WaitForSeconds(0.5f);

        // 销毁角色对象
        // Destroy(gameObject);
    }
    
}



