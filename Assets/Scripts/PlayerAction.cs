using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project
{
	public class PlayerAction : MonoBehaviour
	{
		// Variables
		

		// Functions
		void Start( )
		{

		}
		void Update( )
		{
			//Move the sprite in horizontal direction
			transform.Translate(Input.GetAxis("Horizontal") * 15f * Time.deltaTime, 0f, 0f);

			//Flip the Sprite changing in horizontal direction

			Vector3 characterScale = transform.localScale;
			if (Input.GetAxis ("Horizontal") < 0)
			{
				characterScale.x = -6;
			}
			if (Input.GetAxis ("Horizontal") > 0)
			{
				characterScale.x = 6;
			}
			transform.localScale = characterScale;
		}
	}
}

	
