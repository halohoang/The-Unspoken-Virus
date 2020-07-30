
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
		[SerializeField]
		private float _speed;
		[SerializeField]
		private float _jumpHeight;
		public Transform attackPoint;
		[SerializeField]
		private float _attackRange;
		[SerializeField]
		public Transform shootPoint;
		[SerializeField]
		private int _attackDamage;
		//Ground check variables
		public bool isGrounded = false;
		[SerializeField]
		private GameObject _projectilePrefab;
		[SerializeField]
		private Rigidbody2D _rigidbody2D;



		public LayerMask EnemyLayer;
		// Functions
		//Jump function
		void Jump( )
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_rigidbody2D.AddForce(new Vector2(0f, _jumpHeight), ForceMode2D.Impulse);
				animator.SetBool("IsJumping", true);
				//float jumpvelocity = 100f;
				//rigidbody2d.velocity = Vector2.up*jumpvelocity;


			}
		}
		//Fall Function
		void Fall( )
		{
			if (_rigidbody2D.velocity.y < 0)
			{
				animator.SetBool("IsFalling", true);
				animator.SetBool("IsJumping", false);

			}
		}
		//Attack Function
		public void Attack( )
		{
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, EnemyLayer);

			foreach (Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<IDamageable>().DealDamage(_attackDamage);
			}
		}
		//Deploy Gizmos for attack function
		private void OnDrawGizmos( )
		{
			if (attackPoint == null)
				return;

			Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
		}
		private void Casting( )
		{
			Projectile projectile = Instantiate(_projectilePrefab, shootPoint.position, transform.rotation).GetComponent<Projectile>();
			
		}



		void Update( )
		{
			//shorten the Horizontal input
			float inputHorizontal = Input.GetAxis("Horizontal");
			//Move the sprite in horizontal direction
			transform.Translate(inputHorizontal * _speed * Time.deltaTime, 0f, 0f);

			//Flip the Sprite changing in horizontal direction
			Vector3 characterScale = transform.localScale;
			if (inputHorizontal < 0)
			{
				characterScale.x = -6;
			}
			if (inputHorizontal > 0)
			{
				characterScale.x = 6;
			}
			transform.localScale = characterScale;

			//Start Player moving animation
			animator.SetBool("IsRunning", Mathf.Abs(inputHorizontal) > 0.1f);

			//Related to Groundcheck class
			if (isGrounded)
			{
				//make character jump
				animator.SetBool("IsFalling", false);
				animator.SetBool("IsJumping", false);
				Jump();

			}
			else
			{
				//make character fall
				Fall();
			}


			if (Input.GetMouseButtonDown(1))
			{
				animator.SetTrigger("IsAttacking");
			}

			if (Input.GetMouseButtonDown(0))
			{
				animator.SetTrigger("IsCasting");
			}
		}
	}

}



