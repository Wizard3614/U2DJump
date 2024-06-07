using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ebutton : MonoBehaviour
{
    // 引用目标物体
    public GameObject targetObject;

    public bool change = false;

    // 标记player是否在碰撞范围内
    private bool isPlayerInRange = false;

    // 修改精灵图
    public Sprite newSprite; 

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // 检查玩家是否在范围内并按下E键
        if (isPlayerInRange && Input.GetKeyUp(KeyCode.E) && !change)
        {
            // 获取目标物体上的 SpriteChanger 组件并设置 bool 值
            SpriteChanger targetScript = targetObject.GetComponent<SpriteChanger>();
    
            targetScript.Trigge = true;

            if (spriteRenderer.sprite != newSprite)
            {
                spriteRenderer.sprite = newSprite;
            }

        }
        if (isPlayerInRange && Input.GetKeyUp(KeyCode.E) && change)
        {
            SpriteChanger targetScript = targetObject.GetComponent<SpriteChanger>();

            targetScript.Trigge2 = true;
            if (spriteRenderer.sprite != newSprite)
            {
                spriteRenderer.sprite = newSprite;
            }            
        }
    }

    // 当玩家进入触发器范围时
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // 当玩家离开触发器范围时
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}



