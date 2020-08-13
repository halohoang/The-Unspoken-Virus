using SAE_Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public float _speed;

    private Transform _target;

    public float stoppingDistance;

    [SerializeField]
    private float _visionRange;

    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private float _attackRange;

    LayerMask attackMask;

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
        if (distance < _visionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }
    public void Attack()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, _attackRange, attackMask);

        if (colInfo != null)
        {
            colInfo.GetComponent<PlayersHealth>().DealDamage(_attackDamage);

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _visionRange);
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }


}
