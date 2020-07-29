using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project.Assets.Scripts
{
	public class Projectile : MonoBehaviour
	{

		[SerializeField]
		private int _damage = 1;
		
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out PlayersHealth health))
			{

				health.DealDamage(_damage);


				Destroy(gameObject);
			}



		}
		
		public void Shoot()
		{

		}
	}
}
