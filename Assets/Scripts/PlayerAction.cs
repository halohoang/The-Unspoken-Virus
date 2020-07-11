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
		[SerializeField]
		private float _dashSpeed;
		[SerializeField]
		private float _dashTime;
		[SerializeField]
		private float _startDashTime;
		private int _dashDirection;
		public Transform attackPoint;
		public float attackRange = 0f;
		public int attackDamage = 100;
		//Ground check variables
		public bool isGrounded = false;

		LayerMask EnemyLayer;
		// Functions
		//Jump function
		void Jump( )
		{
			if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
			{
				gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, _jumpHeight), ForceMode2D.Impulse);

			}
		}
		//void Fall( )
		//{
		//	if (isGrounded == false)
		//	{
		//		animator.SetBool("IsFalling", true);
		//		animator.SetBool("IsJumping", false);
		//		animator.SetBool("IsRunning", false);
		//	}
		//	else if (isGrounded == true) ;
		//	{

		//	}
		//}

		void Attack()
		{
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayer);

			foreach (Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<EnemiesHealth>().TakeDamage(attackDamage);
			}
			//animator.SetTrigger(IsAttacking);
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

			//make character jump
			Jump();
			animator.SetBool("IsJumping", Input.GetKeyDown(KeyCode.Space));

			//Fall();

			if (Input.GetMouseButtonDown(1))
			{
				Attack();
			}

			
		}

		//private void OnDrawGizmos()
		//{
		//	if (attackPoint == null)
		//		return;
		//
		//	Gizmos.DrawWireSphere(attackPoint.position, attackRange);
		//}
	}
}


