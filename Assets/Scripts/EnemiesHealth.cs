using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SAE_Project
{
    public class EnemiesHealth : MonoBehaviour, IDamageable
    {   //Variables
        [SerializeField]
        private int _maxHealth;
        [SerializeField]
        private int _currentHealth;
        [SerializeField]
        private Animator _animator;

        public AudioSource Dead;
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
                Dead.Play();
            _animator.SetBool("Dead", true);
                StartCoroutine(Deactive());
                  gameObject.SetActive(false);
                Destroy(gameObject);

            }
        }

      
        IEnumerator Deactive()
        {
            yield return new WaitForSeconds(0.1f);
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Spike"))
            {
                _animator.SetBool("Dead", true);
                StartCoroutine(Deactive());
                Destroy(gameObject);


            }
        }

        public Faction GetFaction()
        {
            return Faction.Enemy;
        }
    }
}