using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project
{
	public abstract class MovementBase : MonoBehaviour
	{
		// Variables
		[SerializeField]
		private float _speed;
		protected Vector2 _direction;

		// Functions
		void Update( )
		{
			CalculatedDirection();
			if (_direction.magnitude != 0)
			{
				_direction = new Vector2(_direction.x / _direction.magnitude, _direction.y / _direction.magnitude);

			}
			transform.Translate(_direction * _speed * Time.deltaTime);
			
		}
		protected abstract void CalculatedDirection();
	}
}

