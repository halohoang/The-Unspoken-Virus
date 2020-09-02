using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SAE_Project
{

    public class MainMenu : MonoBehaviour
    {
        //Variables


        //Functions

        //Pressing Start button

        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Pressing Quit button

        public void QuitGame()
        {
            Debug.Log("Quit Game!!!");
            Application.Quit();


        }




    }

}