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

    void Start()
    {
     
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
        Time.timeScale = 1;
        //场景传输函数

        SceneManager.LoadScene("StartScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

  
}
