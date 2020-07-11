using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SAE_Project
{

    public class EnemiesHealth : MonoBehaviour
    {   //Variables
        public int maxHealth = 100;
        int currentHealth;
        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        // Functions
  
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
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