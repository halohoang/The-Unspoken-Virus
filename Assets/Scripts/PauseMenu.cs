using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SAE_Project
{

    public class PauseMenu : MonoBehaviour
    {
        //Variables
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;



        //Functions
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
            Debug.Log("Back to Menu");
        }

        public void Quit()
        {
            Debug.Log("Quitting Game");
            Application.Quit();

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

}