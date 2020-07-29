using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project
{
    
	public class GroundCheck : MonoBehaviour
	{
		// Variables
		GameObject Player;
		//
		//Hayoon, Please note what i am doing because this is important for your map creation & integration
		//
		public float gravity = 0.8f;
		bool isGrounded;


		// Functions
		void Start( )
		{
			Player = gameObject.transform.parent.gameObject;
		}
		//check if the player has land on the ground yes or no
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.collider.tag == "Ground")
			{
				Player.GetComponent<PlayerAction>().isGrounded = true;
			}
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.collider.tag == "Ground")
			{
				Player.GetComponent<PlayerAction>().isGrounded = false;

			}

		}
	}
}
