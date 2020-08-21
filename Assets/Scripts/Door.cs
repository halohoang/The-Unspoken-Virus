using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int LevelToLoad;
    public GameObject EIcon;
    private bool playerInDoor;

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        EIcon.SetActive(true);
            playerInDoor = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EIcon.SetActive(false);
            playerInDoor = false;

        }


    }

    private void Update()
    {
        if (playerInDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(LevelToLoad);
            }
        }
    }

}
