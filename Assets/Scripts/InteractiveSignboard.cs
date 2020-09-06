using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSignboard : MonoBehaviour
{
    

    private bool _playerInteract;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {


            _playerInteract = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            _playerInteract = false;

        }
    }
}
