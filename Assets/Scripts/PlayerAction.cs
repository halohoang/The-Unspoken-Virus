using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project
{
	public class PlayerAction : MonoBehaviour
	{
		// Variables
		[SerializeField]
		Animator animator;
		// Functions
		void Update( )
		{
			//shorten the Input Horizontal Key
			float InputHorizontal  = Input.GetAxis("Horizontal");

			//Move the sprite in horizontal direction
			transform.Translate(InputHorizontal * 15f * Time.deltaTime, 0f, 0f);
			
			//Flip the Sprite changing in horizontal direction
			Vector3 characterScale = transform.localScale;
			if (InputHorizontal < 0 )
			{
				characterScale.x = -6;
			}
			if (InputHorizontal > 0)
			{
				characterScale.x = 6;
			}
			transform.localScale = characterScale;

			//Start Player moving animation
			animator.SetBool("IsRunning",Mathf.Abs(InputHorizontal) > 0.1f);


		}
	}
}

	
