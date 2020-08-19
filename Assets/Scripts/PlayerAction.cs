
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
        //Variables for melee function
        public Transform attackPoint;
        [SerializeField]
        private float _attackRange;
        [SerializeField]
        private int _attackDamage;
        //Ground check variables
        public bool isGrounded = false;
        //Variables for shooting function
        [SerializeField]
        private GameObject _projectilePrefab;
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        [SerializeField]
        public Transform shootPoint;
        //dashing variables
        [SerializeField]
        private Camera _camera;

        //Dash Variables
        [SerializeField]
        private float dashDistance;
        public bool IsDashing;
        public float DoubleTapTime; //How are you going to initiate the dash function direction 
        [SerializeField]
        public float Waitsleep;
        [SerializeField]
        Rigidbody2D rb;
        KeyCode lastKeyCode;
        private bool _touchedGround = true;
        //private float _

        //Enemy LayerMask
        public LayerMask EnemyLayer;

        //Attack cooldown
        [SerializeField]
        private float _attackCooldown = 1f;
        private float _timer;



        // Functions
        //Move function
        private void Move()
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
        }

        //Dash Function
        private void Dash()
        {
            if (_touchedGround)
            {

                //Dash Left
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (DoubleTapTime > Time.time && lastKeyCode == KeyCode.A)
                    {
                        StartCoroutine(Dash(-1f));
                        animator.SetBool("IsDashing", true);
                    }
                    else
                    {
                        //the amount of second to tap again to actually able to dash
                        DoubleTapTime = Time.time + 0.5f;
                    }

                    lastKeyCode = KeyCode.A;
                }
                
                //Dash Right
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (DoubleTapTime > Time.time && lastKeyCode == KeyCode.D)
                    {
                        StartCoroutine(Dash(1f));
                        animator.SetBool("IsDashing", true);


                    }
                    else
                    {
                        DoubleTapTime = Time.time + 0.5f;
                    }

                    lastKeyCode = KeyCode.D;
                }
               
            }



        }
        IEnumerator Dash(float direction)
        {
            _touchedGround = false;
            IsDashing = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
            float gravity = rb.gravityScale;
            rb.gravityScale = 0;
            yield return new WaitForSeconds(Waitsleep);
            IsDashing = false;
            rb.gravityScale = gravity;
            rb.velocity = Vector3.zero;

        }

        //Jump function
        void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody2D.AddForce(new Vector2(0f, _jumpHeight), ForceMode2D.Impulse);
                animator.SetBool("IsJumping", true);
            }
        }

        //Fall Function
        void Fall()
        {
            if (_rigidbody2D.velocity.y < 0)
            {
                animator.SetBool("IsDashing", false);
                animator.SetBool("IsFalling", true);
                animator.SetBool("IsJumping", false);


            }
        }

        //Attack Function
        public void Attack()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, EnemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<IDamageable>().DealDamage(_attackDamage);
            }
        }
        //Deploy Gizmos for attack function
        private void OnDrawGizmos()
        {
            if (attackPoint == null)
                return;

            Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
        }

        private void Casting()
        {
            //Casting projectiles to direction of mouse cursor
            Vector2 mouseposition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mouseposition);
            Plane plane = new Plane(Vector3.forward, Vector3.zero);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 position = ray.GetPoint(distance);
                Vector3 direction = (position - transform.position).normalized;
                Projectile projectile = new Projectile();
                if (_timer <= 0)
                {
                    projectile = Instantiate(_projectilePrefab, transform.position + direction * 2, transform.rotation).GetComponent<Projectile>();
                    _timer = _attackCooldown;
                }

                projectile.transform.right = direction;
            }
        }
        void Update()
        {
            //Make character dash
            Dash();
            if (!IsDashing)
            {
                Move();
            }

            ////Make Character move
            //Move();

            //Related to Groundcheck class
            if (isGrounded)
            {
                //make character jump
                animator.SetBool("IsFalling", false);
                animator.SetBool("IsJumping", false);
                Jump();
                _touchedGround = true;
            }
            else
            {
                //make character fall
                Fall();
            }

            //Play attack & casting animation
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("IsAttacking");
            }

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("IsCasting");
            }
            //Decrease time each successful casting
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
        }
    }
}



