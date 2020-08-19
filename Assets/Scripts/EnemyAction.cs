using SAE_Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public LayerMask attackMask;

    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        
        float distance;

        Vector3 vector = _target.position - transform.position;
        distance = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
        if (distance < _visionRange && distance > _stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        //if(Vector2.Distance(transform.position, _target.position) > _stoppingDistance)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed *Time.deltaTime);
        //}
            if (Vector2.Distance(transform.position, _target.position) <= _attackRange)
            {
             animator.SetTrigger("Attack");
            }

    }
    public void Attack()
    {
        
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, _attackRange, attackMask);

        if (colInfo != null)
        {
            colInfo.GetComponent<PlayersHealth>().DealDamage(_attackDamage);
            Debug.Log("Damage");

        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _visionRange);
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }


}
