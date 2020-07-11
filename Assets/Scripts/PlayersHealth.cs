using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SAE_Project
{

	public class PlayersHealth : MonoBehaviour
	{
		//Variables 
		public int _currentHealth;
		[SerializeField]
		public int _maxHealth;
		[SerializeField]

		// Functions
		void Update()
		{   //When the game starts the players health will always be the same as the max Health
			_currentHealth = _maxHealth;
		}
		public void DealDamage(int damage)
		{
			//When dealing damage is going to reduce your health and if your health drops to 0 you die ;)
			_currentHealth -= damage;

			if (_currentHealth <= 0)
			{
				Destroy(gameObject);
				Debug.Log(_currentHealth);
			}

			
		}
	}

}