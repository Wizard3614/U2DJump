using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    public GameObject UImenu;
    public GameObject player;
    private float preHealth;
    private PlayerStats playerStats;

    public float fadeDuration = 0.8f; // 淡出效果持续时间
    public Image fadeImage;


    void Start()
    {
        UImenu.SetActive(false);
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.raycastTarget = false;
        playerStats = player.GetComponent<PlayerStats>();
        preHealth = playerStats.MaxHealth;
    }

    void Update()
    {
        
        preHealth = playerStats.Health;
        if (preHealth <= 0 )
        {
            // Debug.Log("死了");
            StartCoroutine(PauseGameAfterDeath());
        }
    }

    private IEnumerator PauseGameAfterDeath()
    {
        yield return new WaitForSeconds(0.5f);
        UImenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, progress));
            progress += rate * Time.deltaTime;
            yield return null;
        }
        // fadeImage.color = new Color(0, 0, 0, 1);

        // AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        // while (!asyncLoad.isDone)
        // {
            // yield return null;
        // }
    }

    public void Mainmenu()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    public IEnumerator FadeOutAndLoadScene()
    {
        Time.timeScale = 1;
        Debug.Log("返回主菜单");
        SceneManager.LoadScene("StartScene");

        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, progress));
            progress += rate * Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1);

        // AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartScene");
        // while (!asyncLoad.isDone)
        // {
        //     yield return null;
        // }
    }

    public void Quit()
    {
        Application.Quit();
    }
}