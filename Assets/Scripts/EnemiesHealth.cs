using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SAE_Project
{
    public class EnemiesHealth : MonoBehaviour, IDamageable
    {   //Variables
        [SerializeField]
        private int _maxHealth = 1;
        [SerializeField]
        private int _currentHealth;
        [SerializeField]
        private Animator _animator;
        void Start()
        {
            _currentHealth = _maxHealth;
        }

        // Functions
        public void DealDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {

            Debug.Log("Enemy died!");
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

        public Faction GetFaction()
        {
            return Faction.Enemy;
        }
    }
}