using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project.Assets.Scripts
{
	public class ProjectileBoss : MonoBehaviour
	{
		public float _speed;

		private Transform _player;

		private Vector2 _target;
		public Faction Team;

		[SerializeField]
		private int _damage;

		[SerializeField]
		private float _range = 10;


		private Transform attackPoint;
		private void Start()
		{
			_player = GameObject.FindGameObjectWithTag("Player").transform;

			_target = new Vector2(_player.position.x, _player.position.y);
		}

		private void Update()
		{
			transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
			if(transform.position.x == _target.x && transform.position.y == _target.y)
			{
				DestroyProjectile();
			}
		}
		private void OnTriggerEnter2D(Collider2D collision)
		{

			if (collision.CompareTag("Player"))
			{
				if (collision.TryGetComponent(out IDamageable damage))
				{
					if (damage.GetFaction() != Team)
					{
						damage.DealDamage(_damage);
						DestroyProjectile();
					}
				}
			}
			
		}
		public float LifeTime => _range / _speed;

		private void OnEnable()
		{
			Destroy(gameObject, LifeTime);
		}

		private void DestroyProjectile()
		{
			Destroy(gameObject);
		}
	}
}
