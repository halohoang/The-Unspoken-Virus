using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project.Assets.Scripts
{
    public class BossHealth : MonoBehaviour, IDamageable
    {

        //Variables
        [SerializeField]
        private int _maxHealth;
        [SerializeField]
        private int _currentHealth;

        public event Action<float> HealthChanged;

        //Sound Effect
        public AudioSource Hurt;
        public AudioSource DieSound;


        void Start()
        {
            _currentHealth = _maxHealth;
        }

        // Functions
        public void DealDamage(int damage)
        {
            Hurt.Play();
            _currentHealth -= damage;

            HealthChanged?.Invoke((float)_currentHealth / _maxHealth);

            if (_currentHealth <= 0)
            {
                DieSound.Play();
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

