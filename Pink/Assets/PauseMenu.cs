using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool pause = false;
    public GameObject UImenu;
    
    public float fadeDuration = 0.8f; // 淡出效果持续时间
    public Image fadeImage;
    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.raycastTarget = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(pause)
            {
                Returngame();
            }

            else
            {
                Pausegame();
            }
        }
    }

    public void Pausegame()
    {
        UImenu.SetActive(true);
        Time.timeScale = 0;
        pause =  true;
    }

    public void Returngame()
    {
        UImenu.SetActive(false);
        Time.timeScale = 1;
        pause = false;
    }

    public void Mainmenu()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }
    public IEnumerator FadeOutAndLoadScene()
    {
        Time.timeScale = 1;
        // 场景传输函数
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // // 渐入新场景
        // progress = 0.0f;
        // while (progress < 1.0f)
        // {
        //     fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, progress));
        //     progress += rate * Time.deltaTime;
        //     yield return null;
        // }
        // fadeImage.color = new Color(0, 0, 0, 0);
        // SceneManager.LoadScene("StartScene");
    }
   
    public void Quit()
    {
        Application.Quit();
    }

  
}
