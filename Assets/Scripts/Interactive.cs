using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive: MonoBehaviour
{
    public GameObject SignBoard;
    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            SignBoard.SetActive(true);
            
        }
    }

   
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
           
    }



}
