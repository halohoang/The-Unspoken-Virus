using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SAE_Project
{

    public class EnemiesHealth : MonoBehaviour
    {   //Variables
        [SerializeField]
        private int _maxHealth =1;
        [SerializeField]
        private int _currentHealth;
        // Start is called before the first frame update
        void Start()
        {
            _currentHealth = _maxHealth;
        }

        // Functions
  
        public void TakeDamage(int damage)
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
            Destroy(gameObject);
        }
    }
}