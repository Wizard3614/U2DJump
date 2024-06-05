using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;
    public float waitTime = 1.0f;
    public float damageAmount = 1f; // 造成的伤害值
    private float arrivalThreshold = 0.5f; // 到达目标点的距离阈值

    public GameObject player; // 主角的GameObject
    public float detectionRange = 1.0f; // 检测范围
    public float playerDistance; // 玩家与敌人之间的距离

    private Vector3 targetPosition;
    private float waitTimer;
    private bool isWaiting = false;

    private Animator animator; // Animator组件
    private Vector2 direction; // 移动方向

    void Start()
    {

        animator = GetComponent<Animator>();

        targetPosition = pointB.position;
    }

    void Update()
    {

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                isWaiting = false;
                targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
            }
        }
        else
        {
            MoveEnemy();
        }

        // 检测敌人与主角之间的距离
        DetectPlayer();

        // 实时计算玩家与敌人之间的距离
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
    }

    void MoveEnemy()
    {
        Vector3 oldPosition = transform.position; // 保存旧位置
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        Vector3 newPosition = transform.position; // 获取新位置

        // 计算移动方向
        direction = new Vector2(newPosition.x - oldPosition.x, newPosition.y - oldPosition.y).normalized;

        if (animator != null)
        {
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", 0);
        }

        if (Vector3.Distance(transform.position, targetPosition) < arrivalThreshold)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }
    }

    void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            FollowAbove.Instance.Show();
            PlayerStats.Instance.TakeDamage(damageAmount);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

