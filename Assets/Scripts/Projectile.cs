using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SAE_Project
{
	public class Projectile : MonoBehaviour
	{
		//Variables
		public Faction Team;
		[SerializeField]
		private int _damage;


		//Functions
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out IDamageable damage))
			{
				if (damage.GetFaction() != Team )
				{
					damage.DealDamage(_damage);
				}
			}
		}
	}
}
