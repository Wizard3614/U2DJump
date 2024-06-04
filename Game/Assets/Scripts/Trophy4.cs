using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Trophy4 : MonoBehaviour
{
    private int currentLevelIndex4 = 4;
    private float fadeDuration = 3f; // 淡出效果持续时间
    public Image fadeImage;
    private AudioSource audioSource;
    private void Start()
    {
        Debug.Log("当前关卡：");
        Debug.Log(currentLevelIndex4);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检测碰撞的对象是否是奖杯
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
        
    }

    public IEnumerator LoadNextScene()
    {
        audioSource.Play();
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;
        // 渐出当前场景
        while (progress < 1.0f)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, progress));
            progress += rate * Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);

        // 加载新场景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level5");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}