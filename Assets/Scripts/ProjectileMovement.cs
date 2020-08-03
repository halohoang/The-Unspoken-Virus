using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project.Assets.Scripts
{
	public class ProjectileMovement : MonoBehaviour
	{
		//Variables
		[SerializeField]
		private float _range = 10;
		protected Vector2 _direction;
		[SerializeField]
		protected float _speed;



		public float LifeTime => _range / _speed;

		//Functions
		private void OnEnable()
		{
			Destroy(gameObject, LifeTime);
		}


		void Update()
		{
			CalculateDirection();

			if (_direction.magnitude != 0)
			{
				_direction = new Vector2(_direction.x / _direction.magnitude, _direction.y / _direction.magnitude);
			}
			transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
		}

		protected void CalculateDirection()
		{
			_direction = transform.right;
		}



	}
}
