using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Trophy : MonoBehaviour
{
    public string[] Levels;
    public static int currentLevelIndex = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检测碰撞的对象是否是奖杯
        if (other.CompareTag("Player"))
        {
            currentLevelIndex++;
            if (currentLevelIndex < Levels.Length)
            {
                SceneManager.LoadScene(Levels[currentLevelIndex]);
            }
            // 加载下一个场景
            else if(currentLevelIndex  > Levels.Length)
            {
                Debug.Log("所有关卡完成！");
            }
        }
    }
}
