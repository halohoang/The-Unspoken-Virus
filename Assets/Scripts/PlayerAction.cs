
using SAE_Project.Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //SoundEffect
        //public AudioSource audio;
        //public AudioClip JumpSoundToPlay;
        //public float Volume;
        //public bool AlreadyPlayed = false;
        public AudioSource JumpSound;
        public AudioSource FireBall;
        public AudioSource Dashing;
        public AudioSource MeleeSwing;
        //public AudioSource WalkSound;
        //public bool AlreadyPlayed = false;
        //public bool IsMoving = false;

        //FadeOut effect
        public FadeOut AfterImage;

        //LoadScene
        public int Index;





        // Functions
        //Move function
        public void Move()
        {
            
            //shorten the Horizontal input
            float inputHorizontal = Input.GetAxis("Horizontal");
            //Move the sprite in horizontal direction
            transform.Translate(inputHorizontal * _speed * Time.deltaTime, 0f, 0f);

            Vector3 characterScale = transform.localScale;
            //Flip the Sprite changing in horizontal direction
            if (inputHorizontal < 0)
            {
                characterScale.x = -8;
                AfterImage.GenerateAfterImages = true;
               // GetComponent<SpriteRenderer>().flipX = true;
                //WalkSound.Play();
            }
           else if (inputHorizontal > 0)
            {
                characterScale.x = 8;
                AfterImage.GenerateAfterImages = true;

                //GetComponent<SpriteRenderer>().flipX = false;
                //WalkSound.Play();

            }
            else
            {
                AfterImage.GenerateAfterImages = false;
            }
            transform.localScale = characterScale;
            

            //Start Player moving animation
            animator.SetBool("IsRunning", Mathf.Abs(inputHorizontal) > 0.1f);

        }

        //Dash Function
        private void Dash()
        {
            if (!isGrounded && _touchedGround )
            {

                //Dash Left
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (DoubleTapTime > Time.time && lastKeyCode == KeyCode.A)
                    {
                        StartCoroutine(Dash(-1f));
                        animator.SetBool("IsDashing", true);
                        Dashing.Play();
                        
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
                        Dashing.Play();

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
                JumpSound.Play();
                
                //if (!AlreadyPlayed)
                //{
                //    audio.PlayOneShot(JumpSoundToPlay, Volume);
                //    AlreadyPlayed = true;
                //}
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
            MeleeSwing.Play();

            //change the vector2.right to the direction where the player is looking 
            RaycastHit2D hitShield = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Abs(transform.position.x - attackPoint.position.x) + _attackRange);

            if (hitShield.collider != null && hitShield.collider.CompareTag("Shield"))
            {
                return;
            }

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
                Projectile projectile;

                //Flip player sprite
                Vector3 mousePosition = Input.mousePosition; // Give mouse position to screenspace
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //change mouse position to worldspace
                Vector3 characterScale = transform.localScale;

                if (mousePosition.x > transform.position.x)
                {
                    characterScale.x = -8;
                    //GetComponent<SpriteRenderer>().flipX = false;

                }
                else
                {
                    characterScale.x = 8;
                    //GetComponent<SpriteRenderer>().flipX = true;

                }

                if (_timer <= 0)
                {
                    FireBall.Play();
                    projectile = Instantiate(_projectilePrefab, transform.position + direction * 2, transform.rotation).GetComponent<Projectile>();
                    _timer = _attackCooldown;
                    projectile.transform.right = direction;
                    
                }
            }
        }
        //public void Start()
        //{
        //    _rigidbody2D = GetComponent<Rigidbody2D>();
        //    WalkSound = GetComponent<AudioSource>();
        //}

        void Update()
        {
 
            if (Input.GetKeyDown("r"))
            {
               
                SceneManager.LoadScene(Index);
            }



            //Make character dash
            Dash();
            if (!IsDashing)
            {
                Move();
            }

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



