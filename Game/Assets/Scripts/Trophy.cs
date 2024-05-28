using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Trophy : MonoBehaviour
{
    public string[] Levels;
    public static int currentLevelIndex = 0;

    private void Start()
    {
        Debug.Log("当前关卡：");
        Debug.Log(currentLevelIndex+1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检测碰撞的对象是否是奖杯
        if (other.CompareTag("Player"))
        {
            StartCoroutine(avoke());
            currentLevelIndex++;
            
            if (currentLevelIndex < Levels.Length)
            {
                SceneManager.LoadScene(Levels[currentLevelIndex]);
            }
            // 加载下一个场景
            else if(currentLevelIndex  >= Levels.Length)
            {
                Debug.Log("所有关卡完成！");
            }
        }
    }

    IEnumerator avoke()
    {
        yield return  new WaitForSeconds(0.5f);
    }
}
