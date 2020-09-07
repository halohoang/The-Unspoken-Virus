using SAE_Project;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SAE_Project
{

    public class EnemyAction : MonoBehaviour
    {
        public float _speed;

        private Transform _target;

        public float _stoppingDistance;

        [SerializeField]
        private float _visionRange;

        public int _attackDamage = 10;

        [SerializeField]
        private float _attackRange;

        [SerializeField]
        private float _blockingRange;

        //block and attack

        public LayerMask attackMask;


        [SerializeField]
        Animator animator;

        //AudioEffect
        //public AudioSource Walk;
        public AudioSource PlayerSpotted;
        public AudioSource ShieldBlock;
        public AudioSource Melee;
        


        // Start is called before the first frame update
        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            gameObject.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            float distance;
            Vector3 vector = _target.position - transform.position;
            distance = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
            if (distance < _visionRange && distance > _blockingRange)
            {
                //Walk.Play();
                PlayerSpotted.Play();
                transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                animator.SetBool("Walk", true);
            }
            else
            {

                animator.SetBool("Walk", false);
            }

            if (_target.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
               // Debug.Log("Turn Left");

            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                //Debug.Log("Turn Right");
            }
            if (distance <= _blockingRange)
            {
                ShieldBlock.Play();
                animator.SetTrigger("Block");
            }
        }
       
        public void OnBlockEnding()
        {

            animator.SetTrigger("Attack");
            Melee.Play();
            StartCoroutine(StopAttack());
        }
        IEnumerator StopAttack()
        {
            yield return new WaitForSeconds(1f);
            animator.SetBool("Attack", false);
        }
        
        public void Attack()
        {

            Collider2D colInfo = Physics2D.OverlapCircle(transform.position, _attackRange, attackMask);



            // Collider2D colInfo = Physics2D.OverlapBox(transform.position, new Vector2(_attackRange, _attackRange), attackMask);


            if (colInfo != null)
            {
                colInfo.GetComponent<PlayersHealth>().DealDamage(_attackDamage);
                Debug.Log("Damage");

            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _visionRange);
            //Gizmos.DrawWireSphere(transform.position, _attackRange);
            //Gizmos.DrawCube(transform.position, new Vector2(_attackRange, _attackRange));
            Gizmos.DrawWireSphere(transform.position, _blockingRange);
        }



    }

}