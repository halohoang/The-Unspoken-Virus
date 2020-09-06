using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive: MonoBehaviour
{
    public GameObject SignBoard;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            SignBoard.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
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
