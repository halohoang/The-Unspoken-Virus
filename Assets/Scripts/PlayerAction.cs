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
		public Rigidbody2D rigidbody2d;
		public BoxCollider2D boxCollider2D;
		//Ground check variables
		public bool isGrounded = false;

		[SerializeField]
		private Rigidbody2D _rigidbody2D;



		LayerMask EnemyLayer;
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
       
        private void FixedUpdate()
        {
			float moveSpeed = 40f;
			rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (Input.GetKey(KeyCode.A))
            {
				rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            }
            else
            {
				if (Input.GetKey(KeyCode.D))
				{
					rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
				}
				else //no key pressed
				{
					rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
					rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                }
		void Fall( )
		{
			if (_rigidbody2D.velocity.y < 0)
			{
				animator.SetBool("IsFalling", true);
				animator.SetBool("IsJumping", false);
				//animator.SetBool("IsRunning", false);
			}
		}

            }
        }
        void Attack()
		{
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayer);

			foreach (Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<EnemiesHealth>().TakeDamage(attackDamage);
			}
			animator.SetTrigger("IsAttacking");
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


			if (Input.GetMouseButton(1))
			{
				Attack();
			}


		}

		private void OnDrawGizmos()
		{
			if (attackPoint == null)
				return;
		
			Gizmos.DrawWireSphere(attackPoint.position, attackRange);
		}
	}
}


