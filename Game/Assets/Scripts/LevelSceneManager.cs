using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSceneManager : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 用于淡入效果的Canvas Group
    public float fadeDuration = 0.8f; // 淡入效果持续时间
    public Image fadeImage;
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // 协程：渐入效果
    private IEnumerator FadeIn()
    {
        // float rate = 1.0f / fadeDuration;
        // float progress = 1.0f;
        //
        // // 将CanvasGroup的alpha值从0渐变到1
        // while (progress <= 1.0f && progress >= 0.0f)
        // {
        //     canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
        //     progress -= rate * Time.deltaTime;
        //     yield return null;
        // }
        //
        // // 确保CanvasGroup的alpha值为0
        // canvasGroup.alpha = 0;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;
        progress = 0.0f;
        while (progress < 1.0f)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, progress));
            progress += rate * Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);
    }
}