using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public float _speed;

    private Transform _target;

    [SerializeField]
    private float _visionRange;

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

        if(distance < _visionRange)
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _visionRange);
    }


}
