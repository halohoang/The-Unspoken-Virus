using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SAE_Project
{
    public class Projectile : MonoBehaviour
    {
        //Variables
        public Faction Team;
        [SerializeField]
        private int _damage;
        private Transform attackPoint;
        private float _attackRange;

        //Functions
        private void OnTriggerEnter2D(Collider2D collision)
        {
            attackPoint = collision.GetComponentInChildren<Transform>();

            RaycastHit2D hitShield = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Abs(transform.position.x - attackPoint.position.x) + _attackRange);

            if (hitShield.collider != null && hitShield.collider.CompareTag("Shield"))
            {
                return;
            }
            if (collision.TryGetComponent(out IDamageable damage))
            {
                if (damage.GetFaction() != Team)
                {
                    damage.DealDamage(_damage);
                }
            }
        }
    }
}
