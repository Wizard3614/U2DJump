using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveconreoller : MonoBehaviour
{
public float moveSpeed = 2f; // 移动速度
    public float moveDistance = 5f; // 移动距离
    private Vector3 initialPosition; // 初始位置
    private Vector3 targetPosition; // 目标位置

    private Vector3 tar;//中间位置方便位置切换

    private Rigidbody2D rb;
    void Start()
    {
        initialPosition = transform.position;
        targetPosition =  initialPosition;
        targetPosition.x = initialPosition.x + moveDistance; 
        tar = targetPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 移动障碍物
        rb.position = Vector3.MoveTowards(transform.position, tar, moveSpeed * Time.deltaTime);
    
        // 到达目标位置后，切换目标位置
        if (Vector3.Distance(transform.position, tar) < 0.1f)
        {
            SwitchTarget();
        }
    }

    // 切换目标位置
    void SwitchTarget()
    {
        if ( this.transform.position == targetPosition )
        {
            tar = initialPosition;
        }

        else if ( this.transform.position == initialPosition )
        {
            tar = targetPosition;
        }
    }

}
