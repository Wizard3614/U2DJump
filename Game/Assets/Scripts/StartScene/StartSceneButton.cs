using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartSceneButton : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    public Image fadeImage;
    public float fadeDuration = 0.8f; // 淡出效果持续时间

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.raycastTarget = false;
    }

    public void OnButtonClick1()
    {
        PlayClickSound();
        StartCoroutine(FadeOutAndLoadScene1());
    }
    public void OnButtonClick2()
    {
        PlayClickSound();
        StartCoroutine(FadeOutAndLoadScene2());
    }
    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
    private IEnumerator FadeOutAndLoadScene1()
    {
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 渐入新场景
        // progress = 0.0f;
        // while (progress < 1.0f)
        // {
        //     fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, progress));
        //     progress += rate * Time.deltaTime;
        //     yield return null;
        // }
        // fadeImage.color = new Color(0, 0, 0, 0);
    }
    private IEnumerator FadeOutAndLoadScene2()
    {
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level2");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 渐入新场景
        // progress = 0.0f;
        // while (progress < 1.0f)
        // {
        //     fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, progress));
        //     progress += rate * Time.deltaTime;
        //     yield return null;
        // }
        // fadeImage.color = new Color(0, 0, 0, 0);
    }
}
